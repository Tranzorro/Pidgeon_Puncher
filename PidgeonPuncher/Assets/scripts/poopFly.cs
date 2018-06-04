using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poopFly : MonoBehaviour 
{
    public Transform Player;
    public float speed = 2f;

    void Update()
    {
        Player = GameObject.FindWithTag("Player").transform;
        transform.position = Vector3.MoveTowards(transform.position, Player.position, speed * Time.deltaTime);

    }
}