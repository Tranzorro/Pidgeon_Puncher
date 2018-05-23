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
        Mesh planeMesh = gameObject.GetComponent<MeshFilter>().mesh;
        Bounds bounds = planeMesh.bounds;

        float minX = gameObject.transform.position.x - gameObject.transform.localScale.x * bounds.size.x * 0.5f;
        float minZ = gameObject.transform.position.z - gameObject.transform.localScale.z * bounds.size.z * 0.5f;

        Vector2 newVec = new Vector3(Random.Range (minX, -minX), gameObject.transform.position.y, Random.Range (minZ, -minZ));
        return newVec;
    }

    void Spawn()
    {
        //Instantiate a pigeon
        Instantiate(pigeon, spawnPlane.transform.position/*GetRandomSpawnPos()*/, spawnPlane.transform.rotation, player.transform);
    }
}