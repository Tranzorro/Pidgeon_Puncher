﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playGame : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("pidgeon-Puncher", LoadSceneMode.Single);

    }

    public void QuitGame()
    {
        Application.Quit();
    }

}