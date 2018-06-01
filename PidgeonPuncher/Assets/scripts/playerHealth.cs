using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour {

    public float CurrentHealth;
    public float MaxHealth;

    public  Slider Healthbar;


    public void ChangeHealth(float amount)
    {
        CurrentHealth += amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, MaxHealth);

        // Healthbar.value = CurrentHealth / MaxHealth;
        Healthbar.value = CurrentHealth;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pigeon")
            ChangeHealth(-1f);
        if (CurrentHealth <= 0f)
            Die();
    }

    void Die()
    {
        //CurrentHealth = 0;
        Debug.Log("Grossed out by pigeons, Game Over.");
        SceneManager.LoadScene("GameOverMenu", LoadSceneMode.Single);
    }
}
