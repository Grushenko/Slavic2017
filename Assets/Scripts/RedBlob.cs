﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBlob : Blob, ISoundReactive
{
    public GameObject blobPrefab;
    public Animator animator;
    private AudioSource audioSource;

    public AudioClip chargeSound;
    private AudioClip basicSound; 

    // Use this for initialization
    override protected void Start()
    {
        base.Start();
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
        basicSound = audioSource.clip;
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
    }

    public void reactOnSound(Player player)
    {
        addChargeForce(player.gameObject.transform.position - transform.position, 20 * movementSpeed);
        //base.moveToPosition(player.gameObject.transform.position, base.movementSpeed, 2f);
        animator.SetTrigger("Happy");
        audioSource.clip = chargeSound;
        audioSource.Play();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != null && collision.gameObject.GetComponent<Blob>() != null &&
            collision.gameObject.GetComponent<PurpleBlob>() == null
            && collision.gameObject.GetComponent<RedBlob>() == null &&
            collision.gameObject.GetComponent<BlackBlob>() == null)
        {
            Destroy(collision.gameObject);
            GameObject blob = Instantiate(blobPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (collision.gameObject.GetComponent<Player>() != null)
        {
            var player = collision.gameObject.GetComponent<Player>();

            var playerBody = player.GetComponent<Rigidbody>();
            playerBody.AddForce(player.baseForce * 0.5f  * -playerBody.velocity);
            GetComponent<AudioSource>().Play();
            animator.SetTrigger("Upset");
            audioSource.clip = basicSound;
            audioSource.Play();
        }
    }
}