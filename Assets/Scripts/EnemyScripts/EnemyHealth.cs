using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    float health;

    GameObject soundManager;

    void Start()
    {
        health = 5;
        soundManager = GameObject.Find("SoundManager");
    }

    private void TakeDamage(float damage)
    {
        if (health > 1)
        {
            soundManager.GetComponent<SoundManager>().hit1Play();
        }
        else
        {
            soundManager.GetComponent<SoundManager>().hit2Play();
            soundManager.GetComponent<SoundManager>().enemyDeathSoundPlay();
        }

        health -= damage;

        if (health <= 0) {
            Destroy(this.gameObject);
        }
    }
}
