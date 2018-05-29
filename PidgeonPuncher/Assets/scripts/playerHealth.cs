using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour {

    public float CurrentHealth { get; set; }
    public float MaxHealth { get; set; }

    public  Slider Healthbar;




    // Use this for initialization
    void Start () {
        // reset hp to max at start
        Debug.Log("health restored. good luck!");
        MaxHealth = 3f;

        CurrentHealth = MaxHealth;
        //Healthbar.value = CalculateHealth();
        
	}
    void DealDamage(float damageValue)
    {
        Debug.Log("you took damage!");
        CurrentHealth -= damageValue;
        Healthbar.value = CalculateHealth();

        if (CurrentHealth <= 0)
            Die();
    }
    float CalculateHealth()
    {
        return CurrentHealth / MaxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pigeon")
            DealDamage(1);
    }


    void Die()
    {
        CurrentHealth = 0;
        Debug.Log("Grossed out by pigeons, Game Over.");
        SceneManager.LoadSceneAsync("GameOverMenu");
    }
}
