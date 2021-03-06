﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobSpawner : MonoBehaviour {
    public GameObject blob_prefab;
    public int max_blobs_number;
    public float check_interval;
    public float spawn_radius;

    public List<GameObject> list_of_blobs;

	void Start () {
        InvokeRepeating("RestoreBlobs", 1.0f, check_interval);
        list_of_blobs = new List<GameObject>();
    }

	void Update () {
		
	}

    void RestoreBlobs () {
        if(ShouldSpawnBlob()) {
            SpawnBlob(); 
        }
    }

    bool ShouldSpawnBlob () {
        return GetNumberOfLivingBlobs() < max_blobs_number ;
    }

    void SpawnBlob() {
        Vector3 spawn_vector = Random.insideUnitCircle * spawn_radius;

        spawn_vector.z = spawn_vector.y;
        spawn_vector.y = 0;

        GameObject blob = Instantiate(blob_prefab, transform.position + spawn_vector, Quaternion.identity, transform);
        list_of_blobs.Add(blob);
    }

    public void BlobDied(GameObject blob) {
        list_of_blobs.Remove(blob);
    }

    int GetNumberOfLivingBlobs() {
        return list_of_blobs.Count;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, spawn_radius);
    }
}
