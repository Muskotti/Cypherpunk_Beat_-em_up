﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    GameObject soundManager;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(2.0f);

        soundManager = GameObject.Find("SoundManager");
        soundManager.GetComponent<SoundManager>().menuThemePlay();
    }

    public void playGame()
    {
        soundManager.GetComponent<SoundManager>().ButtonClickPlay();
        SceneManager.LoadScene("LoadingScene");
    }
    
    public void quitGame()
    {
        soundManager.GetComponent<SoundManager>().ButtonClickPlay();
        Debug.Log("QUIT!");
        Application.Quit();
    }
}