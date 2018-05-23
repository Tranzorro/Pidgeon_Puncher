using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killCollide : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        //check collision name
        //Debug.Log("collision name = " + other.gameObject.name);
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("de-rezzing self");
            Destroy(gameObject);
        }

    }
}
