using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playGame : MonoBehaviour
{
    public AudioSource musicVol;
    public AudioSource sfxVol;

    public void PlayGame()
    {
        SceneManager.LoadScene("test_Scene", LoadSceneMode.Single);
        musicVol.volume = PlayerSave.GetFloat("musicVol");
        sfxVol.volume = PlayerSave.GetFloat("sfxVol");

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOverMenu", LoadSceneMode.Single);

    }

    public void Options()
    {
        SceneManager.LoadScene("Options", LoadSceneMode.Single);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
    }

}