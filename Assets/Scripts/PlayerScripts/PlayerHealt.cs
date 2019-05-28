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

    private void Awake()
    {
        currentHealth = startHealt;
        playerMovement = GetComponent<CharacterController>();
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
