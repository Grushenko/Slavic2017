﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobSpawner : MonoBehaviour {
    public GameObject blob_prefab;
    public int max_blobs_number;
    public float check_interval;

    List<GameObject> list_of_blobs;

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
        Debug.Log("Spawn blob");
        GameObject blob = Instantiate(blob_prefab, transform.position, Quaternion.identity, transform);
        list_of_blobs.Add(blob);
    }

    void BlobDied(GameObject blob) {
        list_of_blobs.Remove(blob);
    }

    int GetNumberOfLivingBlobs() {
        return list_of_blobs.Count;
    }
}
