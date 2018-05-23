using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class punch : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        //check collision name
        //Debug.Log("collision name = " + other.gameObject.name);
        if (other.gameObject.tag == "Pigeon")
        {
            Debug.Log("PUNCHED!!!!");
            Destroy(other.gameObject);
        }

    }
}
