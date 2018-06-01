using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class punch : MonoBehaviour {

    public Text PunchCounter;
    public int PunchCount;
    void Start()
    {
        PunchCount = 0;
        SetCountText();
    }

    void OnTriggerEnter(Collider other)
    {
        //check collision name
        //Debug.Log("collision name = " + other.gameObject.name);
        if (other.gameObject.tag == "Pigeon")
        {
            Debug.Log("PUNCHED!!!!");
            Destroy(other.gameObject);
            PunchCount += 1;
            SetCountText();
        }

    }

    void SetCountText()
    {
        PunchCounter.text = "" + PunchCount.ToString();
    }
}
