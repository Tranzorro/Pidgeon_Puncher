using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pigeonSpawn : MonoBehaviour
{
    public GameObject spawnPlane;
    //The amount of time between each spawn.
    public float spawnTime = 5f;
    //The amount of time before spawning starts.
    public float spawnDelay = 3f;
    public GameObject pigeon;
    public GameObject player;

    void Start()
    {
        //Start calling the Spawn function repeatedly after a delay.
        InvokeRepeating("Spawn", spawnDelay, spawnTime);
    }

    void Spawn()
    {
        //Instantiate a pigeon
        Instantiate(pigeon, spawnPlane.transform.position, spawnPlane.transform.rotation, player.transform);
    }
}