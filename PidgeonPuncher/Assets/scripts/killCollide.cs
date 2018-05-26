using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class killCollide : MonoBehaviour{

   // public GameObject healthbar;

    //private playerHealth refscript;

    void OnTriggerEnter(Collider other)
    {

        //check collision name
        //Debug.Log("collision name = " + other.gameObject.name);
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("touched player. now i am the dead.");
            DealDamage(1);
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Horizon")
        {
            Debug.Log("GROUND PIGEONS ARE NOT ALLOWED");
            Destroy(gameObject);
        }
    }
    public static void DealDamage(float damageValue)
    {
        
        playerHealth.CurrentHealth -= damageValue;
        playerHealth.Healthbar.value = playerHealth.CalculateHealth();

        if (playerHealth.CurrentHealth <= 0)
            playerHealth.Die();
    }


    
}
