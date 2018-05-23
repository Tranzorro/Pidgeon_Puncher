using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pigeonFly : MonoBehaviour
{
    public Transform Player;
    public float speed = 2f;
    private float minDistance = 0.2f;
    private float range;

    void Update()
    {
        Player = GameObject.FindWithTag("Player").transform;
        range = Vector2.Distance(transform.position, Player.position);
        if (range > minDistance)
        {
            Debug.Log(range);
            Player = GameObject.FindWithTag("Player").transform;
            transform.position = Vector2.MoveTowards(transform.position, Player.position, speed * Time.deltaTime);
        }
    }
}