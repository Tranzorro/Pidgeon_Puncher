﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killCollide : MonoBehaviour{


    void OnTriggerEnter(Collider other)
    {

        //check collision name
        //Debug.Log("collision name = " + other.gameObject.name);
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("touched player. now i am the dead.");
            Destroy(this.gameObject);
        }
        if (other.gameObject.tag == "Horizon")
        {
            Debug.Log("GROUND PIGEONS ARE NOT ALLOWED");
            Destroy(this.gameObject);
        }
        if (other.gameObject.tag == "Killzone")
        {
            Debug.Log("hit the fan, splat!");
            Destroy(this.gameObject);
        }
    }
    
}
