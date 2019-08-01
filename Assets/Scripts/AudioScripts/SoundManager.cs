using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource audioSource;

    // Combat sounds
    public AudioClip Uuf;
    public AudioClip hitSound1;
    public AudioClip hitSound2;
    public AudioClip enemyDeath;

    // Pickup sounds
    public AudioClip buttonClick;
    public AudioClip creditPickup;
    public AudioClip keycardPickup;
    public AudioClip upgradeSound;

    // Music
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

    // Combat sound effects
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

    // Pickup sound effects
    public void ButtonClickPlay()
    {
        audioSource.pitch = 1f;
        audioSource.PlayOneShot(buttonClick, 1f);
    }

    public void CreditPickupPlay()
    {
        audioSource.PlayOneShot(creditPickup, 0.7f);
        audioSource.pitch = 1f;
    }

    public void KeycardPickupPlay()
    {
        audioSource.PlayOneShot(keycardPickup, 2f);
        audioSource.pitch = 1f;
    }

    public void UpgradePlay()
    {
        audioSource.PlayOneShot(upgradeSound, 0.7f);
        audioSource.pitch = 1f;
    }
}
