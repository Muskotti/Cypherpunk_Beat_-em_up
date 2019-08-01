using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMM : MonoBehaviour
{
    GameObject player;

    public void Start()
    {
        player = GameObject.Find("Player");
    }

    public void MainMenu()
    {
        Debug.Log("asdasd");
        SceneManager.LoadScene("MainMenu");
    }
}

        Time.timeScale = 1;