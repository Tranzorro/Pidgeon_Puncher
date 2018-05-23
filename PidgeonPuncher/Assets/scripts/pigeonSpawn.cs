using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pigeonSpawn : MonoBehaviour
{

    public float spawnTime = 5f;
    //The amount of time between each spawn.
    public float spawnDelay = 3f;
    //The amount of time before spawning starts.
    public GameObject[] pigeon;
    //Array of enemy prefabs.
    public Vector3 enposition;

    void Start()
    {
        //Start calling the Spawn function repeatedly after a delay.
        InvokeRepeating("Spawn", spawnDelay, spawnTime);
    }

    void Spawn()
    {
        //Instantiate a random enemy.
        int enemyIndex = Random.Range(0, pigeon.Length);
        Instantiate(pigeon[enemyIndex], enposition, transform.rotation);
    }
}