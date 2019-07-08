using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    public bool pauseToggle;
    public GameObject pauseUI;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseToggle)
            {
                Time.timeScale = 1;
                pauseUI.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                pauseUI.SetActive(true);
            }

            pauseToggle = !pauseToggle;
        }
    }
}
