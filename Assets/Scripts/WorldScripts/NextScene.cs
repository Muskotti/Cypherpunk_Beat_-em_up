using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public string nextScene;

    public void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("NextScene");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().name.Equals("MainMenu"))
        {
            nextScene = "HospitalScene";
        }
        else if (SceneManager.GetActiveScene().name.Equals("HospitalScene"))
        {
            nextScene = "SampleScene";
        }
        else if (SceneManager.GetActiveScene().name.Equals("SampleScene"))
        {
            nextScene = "MainMenu";
        }
    }
}
