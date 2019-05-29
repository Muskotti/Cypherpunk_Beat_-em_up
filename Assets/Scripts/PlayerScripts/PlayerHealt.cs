using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealt : MonoBehaviour
{
    public int startHealt = 5;
    public int currentHealth;

    CharacterController playerMovement;

    bool damage;
    bool isDead;

    GameObject soundManager;

    private void Awake()
    {
        currentHealth = startHealt;
        playerMovement = GetComponent<CharacterController>();
        soundManager = GameObject.Find("SoundManager");
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
        
        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;
        playerMovement.enabled = false;
    }
}
