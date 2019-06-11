using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealt : MonoBehaviour
{
    public int startHealt = 5;
    public int currentHealth;

    CharacterController playerMovement;
    public GameObject deathScreen;

    bool damage;
    public bool isDead;

    GameObject soundManager;

    public GameObject playerhit;

    private void Awake()
    {
        currentHealth = startHealt;
        playerMovement = GetComponent<CharacterController>();
        soundManager = GameObject.Find("SoundManager");
        deathScreen.SetActive(false);
    }

    private void Update()
    {
        if(damage)
        {
            // set playerer to lover healt sprite
        }
    }

    public void TakeDamage(int amount)
    {
        soundManager.GetComponent<SoundManager>().takeDamagePlay();
        damage = true;
        currentHealth -= amount;
        GameObject hitmarker = Instantiate(playerhit, transform.position, Quaternion.identity) as GameObject;
        Destroy(hitmarker, 0.2f);
        
        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;
        playerMovement.enabled = false;
        deathScreen.SetActive(true);
    }
}
