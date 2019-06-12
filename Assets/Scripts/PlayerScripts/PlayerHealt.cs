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

    public Sprite player80;
    public Sprite player60;
    public Sprite player40;
    public Sprite player20;

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
        
        switch (currentHealth) {
            case 4:
                this.GetComponent<SpriteRenderer>().sprite = player80;
                break;
            case 3:
                this.GetComponent<SpriteRenderer>().sprite = player60;
                break;
            case 2:
                this.GetComponent<SpriteRenderer>().sprite = player40;
                break;
            case 1:
                this.GetComponent<SpriteRenderer>().sprite = player20;
                break;
        }

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
