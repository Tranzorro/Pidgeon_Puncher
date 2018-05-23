using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killCollide : MonoBehaviour {

    void OnCollisionEnter(Collision col)
    {
        //check collision name
        Debug.Log("collision name = " + col.gameObject.name);
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("de-rezzing self");
            Destroy(gameObject);
        }

    }
}
