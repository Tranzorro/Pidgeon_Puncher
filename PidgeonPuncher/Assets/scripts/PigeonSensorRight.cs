using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonSensorRight : MonoBehaviour {

    public GameObject rightScreen;

    // when pigeon enters right screen, show alert for RIGHT side
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pigeon")
        {
            Debug.Log("Pigeon on the right!!!");
            rightScreen.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Pigeon")
        {
            rightScreen.SetActive(false);
        }
    }
}
