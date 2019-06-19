using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    float health;

    GameObject soundManager;

    public GameObject enemyhit;
    public GameObject enemydead;

    void Start()
    {
        health = 5;
        soundManager = GameObject.Find("SoundManager");
    }

    private void TakeDamage(float damage)
    {

        if (health > 1)
        {
            GameObject hitmarker = Instantiate(enemyhit, transform.position + new Vector3(0.0f, 0.1f, 0.0f), Quaternion.identity) as GameObject;
            Destroy(hitmarker, 1f);
            soundManager.GetComponent<SoundManager>().hit1Play();
        }
        else
        {
            soundManager.GetComponent<SoundManager>().hit2Play();
            soundManager.GetComponent<SoundManager>().enemyDeathSoundPlay();
            GameObject hitmarker = Instantiate(enemydead, transform.position + new Vector3(0.0f, 0.1f, 0.0f), Quaternion.identity) as GameObject;
            Destroy(hitmarker, 1f);
        }

        health -= damage;

        if (health <= 0) {
            Destroy(this.gameObject);
        }
    }
}
