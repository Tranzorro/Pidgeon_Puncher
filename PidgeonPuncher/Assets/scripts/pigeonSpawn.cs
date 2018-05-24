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

    public Vector3 GetRandomSpawnPos()
    {
        Mesh spawnPlane = gameObject.GetComponent<MeshFilter>().mesh;
        Bounds bounds = spawnPlane.bounds;

        float minX = gameObject.transform.localPosition.x - gameObject.transform.localScale.x * bounds.size.x * 0.5f;
        float minY = gameObject.transform.localPosition.z - gameObject.transform.localScale.z * bounds.size.z * 0.5f;

        Vector3 newVec = new Vector3(Random.Range (minX, -minX), Random.Range(minY, -minY), gameObject.transform.position.z);
        return newVec;
    }

    void Spawn()
    {
        //Instantiate a pigeon
        Instantiate(pigeon, GetRandomSpawnPos(), spawnPlane.transform.rotation, player.transform);
    }
}