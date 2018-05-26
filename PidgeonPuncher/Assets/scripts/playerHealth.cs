using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour {

    public static float CurrentHealth { get; set; }
    public static float MaxHealth { get; set; }

    public static Slider Healthbar;

	// Use this for initialization
	void Start () {
        // reset hp to max at start
        MaxHealth = 20f;

        CurrentHealth = MaxHealth;
        Healthbar.value = CalculateHealth();
        
	}

    public static float CalculateHealth()
    {
        return CurrentHealth / MaxHealth;
    }
    public static void Die()
    {
        CurrentHealth = 0;
        SceneManager.LoadScene("GameOverMenu");
    }
}
