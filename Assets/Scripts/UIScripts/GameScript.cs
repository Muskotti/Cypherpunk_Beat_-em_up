using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    GameObject soundManager;
    
    void Start()
    {
        soundManager = GameObject.Find("SoundManager");
        soundManager.GetComponent<SoundManager>().battleThemePlay();
    }

    void Update()
    {
        
    }
}
