using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playGame : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("test_Scene", LoadSceneMode.Single);

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOverMenu", LoadSceneMode.Single);

    }

}