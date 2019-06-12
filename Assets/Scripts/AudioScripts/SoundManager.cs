using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource audioSource;

    public AudioClip Uuf;

    public GameObject menuTheme;
    public GameObject battleTheme;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Music
    public void menuThemePlay()
    {
        menuTheme.SetActive(true);
        battleTheme.SetActive(false);
    }
    public void battleThemePlay()
    {
        battleTheme.SetActive(true);
        menuTheme.SetActive(false);
    }

    // Sound effects
    public void takeDamagePlay()
    {
        audioSource.pitch = 1f;
        audioSource.PlayOneShot(Uuf, 2f);
    }
}
