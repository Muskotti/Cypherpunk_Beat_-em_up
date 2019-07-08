using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    GameObject soundManager;

    void Start()
    {
        soundManager = GameObject.Find("SoundManager");
        soundManager.GetComponent<SoundManager>().menuThemePlay();
    }

    public void playGame()
    {
        SceneManager.LoadScene("HospitalScene");
    }
    
    public void quitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}