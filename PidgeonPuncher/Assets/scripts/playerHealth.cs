using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour {

    public AudioClip splat;
    private AudioSource source;
    public float CurrentHealth;
    public float MaxHealth;
    public GameObject[] Poo;
    GameObject currentpoo;
    public RectTransform Gui;
    int index;

    
    public  Slider Healthbar;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void ChangeHealth(float amount)
    {
        CurrentHealth += amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, MaxHealth);

        // Healthbar.value = CurrentHealth / MaxHealth;
        Healthbar.value = CurrentHealth;
    } 


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "poop")
            ChangeHealth(-1f);
            Debug.Log("gross, you got pooped on!");
            RandoPoo();
            Debug.Log("poop smeared away, but you feel gross.");
            source.PlayOneShot(splat, 1F);
        if (CurrentHealth <= 0f)
            Die();
    }
    
    void Die()
    {
        //CurrentHealth = 0;
        Debug.Log("Grossed out by pigeons, Game Over.");
        SceneManager.LoadScene("GameOverMenu", LoadSceneMode.Single);
    }

    void RandoPoo()
    {
        
        index = Random.Range(0, Poo.Length);
        currentpoo = Poo[index];
        Vector3 spawnPosition = GetBotomLeftCorner(Gui) - new Vector3(Random.Range(0, Gui.rect.x), Random.Range(0, Gui.rect.y), 0);
        Instantiate(currentpoo, spawnPosition, Quaternion.identity, Gui);
        //newPoo.SetActive(true);  //don't use this here, it makes it lag for some reason, not sure why.
    }

    Vector3 GetBotomLeftCorner(RectTransform rt)
    {
        Vector3[] v = new Vector3[4];
        rt.GetWorldCorners(v);
        return v[0];
    }
}
