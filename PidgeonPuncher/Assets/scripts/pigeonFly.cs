using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pigeonFly : MonoBehaviour
{

    public randoSpawn poopScript;
    // the collider that the pigeon hits before hovering for a short time
    public GameObject hoverPlane;
    public Transform Player;
    public float speed = 2f;

    private void Start()
    {

        poopScript = GetComponent<randoSpawn>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HoverPlane")
        {
            Debug.Log("HOVERING");
            StartCoroutine(FreezePigeon());
        }
    }

    IEnumerator FreezePigeon()
    {
        Debug.Log("I am in the hovering code block");
        speed = 0f;
        poopScript.enabled = true;
        yield return new WaitForSeconds(5);
         Debug.Log("I POOP ON YOU");
        poopScript.enabled = false;
        speed = -4f;
        Debug.Log("I have completed the hovering code block");
    }


    void Update()
    {
        poopScript.enabled = false;
        //Player = GameObject.FindWithTag("Player").transform;
            Player = GameObject.FindWithTag("Player").transform;
            transform.position = Vector3.MoveTowards(transform.position, Player.position, speed * Time.deltaTime);
        
    }
}