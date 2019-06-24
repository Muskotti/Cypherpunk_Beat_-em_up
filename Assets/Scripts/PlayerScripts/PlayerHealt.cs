using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealt : MonoBehaviour
{
    public int startHealt = 5;
    public int currentHealth;
    
    public GameObject deathScreen;

    bool damage;
    public bool isDead;

    GameObject soundManager;

    public GameObject playerhit;
    private GameObject player;

    public Animator animator;

    private void Awake()
    {
        currentHealth = startHealt;
        soundManager = GameObject.Find("SoundManager");
        deathScreen.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void TakeDamage(int amount)
    {
        soundManager.GetComponent<SoundManager>().takeDamagePlay();
        damage = true;
        currentHealth -= amount;
        GameObject hitmarker = Instantiate(playerhit, transform.position + new Vector3(0.0f,0.1f, 0.0f), Quaternion.identity) as GameObject;
        Destroy(hitmarker, 0.2f);
        
        switch (currentHealth) {
            case 4:
                animator.SetTrigger("idle80Trigger");
                break;
            case 3:
                animator.SetTrigger("idle60Trigger");
                break;
            case 2:
                animator.SetTrigger("idle40Trigger");
                break;
            case 1:
                animator.SetTrigger("idle20Trigger");
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
        deathScreen.SetActive(true);
        player.SendMessage("SetMoveStatus",false);
        player.SendMessage("SetDeadStatus",true);
    }
}
