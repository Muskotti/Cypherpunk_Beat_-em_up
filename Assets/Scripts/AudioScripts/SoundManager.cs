using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource audioSource;

    public AudioClip Uuf;
    public AudioClip hitSound1;
    public AudioClip hitSound2;
    public AudioClip enemyDeath;

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

    public void hit1Play()
    {
        audioSource.pitch = 1f;
        audioSource.PlayOneShot(hitSound1, 2f);
    }

    public void hit2Play()
    {
        audioSource.pitch = 1f;
        audioSource.PlayOneShot(hitSound2, 2f);
    }

    public void enemyDeathSoundPlay()
    {
        audioSource.pitch = 1f;
        audioSource.PlayOneShot(enemyDeath, 1f);
    }
}
