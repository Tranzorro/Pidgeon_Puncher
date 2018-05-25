﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randoSpawn : MonoBehaviour {

    public GameObject spawner;
    //The amount of time between each spawn.
    public float spawnTime = 0f;
    //The amount of time before spawning starts.
    public float spawnDelay = 0f;
    // the object to spawn
    public GameObject pigeon;
    // what to parent to after spawning
    public GameObject player;

    // Use this for initialization
    void Start () {
        //Start calling the Spawn function repeatedly after a delay.
        InvokeRepeating("Spawn", spawnDelay, spawnTime);
    }

    void Spawn()
    {
        spawnTime = Random.Range(0f, 2f);
        spawnDelay = Random.Range(0f, 1f);

        //Instantiate a pigeon
        Instantiate(pigeon, spawner.transform.position, spawner.transform.localRotation, player.transform);
    }
}