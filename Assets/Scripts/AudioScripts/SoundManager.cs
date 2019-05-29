using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource audioSource;

    public AudioClip Uuf;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Sound effects
    public void takeDamagePlay()
    {
        audioSource.pitch = 1f;
        audioSource.PlayOneShot(Uuf, 2f);
    }
}
