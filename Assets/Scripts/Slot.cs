﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour {

    public GameObject replace_object;
    public Blob wanted_component;
    public Vector3 pulling_end_diff_vector;
    public float pulling_duration;

    GameObject pulling_blob;
    Vector3 pulling_start_position;
    float pulling_start_time;

    bool isCompleted;
    bool isPulling;

    void Start () {
        isCompleted = false;
        isPulling = false;
	}

	void Update () {
        if (isPulling) {
            PullBlob();
            if (pulling_blob.transform.position == transform.position + pulling_end_diff_vector) {
                EndPullingBlob();
            }
        }
	}

    void OnTriggerEnter(Collider other) {
        if (IsThisWantedBlob(other.gameObject)) {
            SetPullingBlob(other.gameObject);
            Debug.Log("same type");
            GetComponent<Collider>().enabled = false;
        }
    }

    bool IsCompleted() {
        return isCompleted;
    }

    void SetPullingBlob(GameObject blob) {
        blob.GetComponent<Blob>().enabled = false;
  
        pulling_blob = blob;
        pulling_start_position = pulling_blob.transform.position;
        pulling_start_time = Time.time;
        isPulling = true;
    }

    void PullBlob() {
        pulling_blob.transform.position = Vector3.Lerp(pulling_start_position, transform.position + pulling_end_diff_vector, (Time.time - pulling_start_time) / pulling_duration);
    }

    void EndPullingBlob() {
        isPulling = false;
        Destroy(pulling_blob);
        Instantiate(replace_object, transform.position, Quaternion.identity, transform);

        isCompleted = true;
        transform.parent.GetComponent<Obelisk>().ReportSuccess();
    }

    bool IsThisWantedBlob(GameObject blob) {
        Blob comp = blob.GetComponent<Blob>();
        return wanted_component.GetType().Equals(comp.GetType());
    }
}