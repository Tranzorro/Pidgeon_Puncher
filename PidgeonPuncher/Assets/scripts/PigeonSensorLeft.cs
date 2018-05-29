using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonSensorLeft : MonoBehaviour {

    public GameObject leftScreen;

    // when pigeon enters left screen, show alert for LEFT side
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pigeon")
        {
            Debug.Log("Pigeon on the left!!!");
            leftScreen.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Pigeon")
        {
            leftScreen.SetActive(false);
        }
    }
}
