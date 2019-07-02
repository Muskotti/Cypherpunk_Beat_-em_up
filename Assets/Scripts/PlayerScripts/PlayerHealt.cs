using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealt : MonoBehaviour
{
    public int startHealt = 5;
    public int currentHealth;
    bool isStunned;

    public GameObject deathScreen;

    bool damage;
    public bool isDead;

    GameObject soundManager;

    public GameObject playerhit;
    private GameObject player;

    // Stun
    public float stunTimer;
    public float stunCountdown;

    public Animator animator;

    private void Awake()
    {
        currentHealth = startHealt;
        stunCountdown = stunTimer;
        isStunned = false;

        soundManager = GameObject.Find("SoundManager");
        deathScreen.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Update()
    {
        if (isStunned)
        {
            stunCountdown = stunCountdown -= Time.deltaTime;
        }

        if (stunCountdown <= 0)
        {
            if (!isDead)
            {
                isStunned = false;
                animator.SetBool("isStunned", false);

                // Enable scripts after stun ends
                GetComponent<Movement>().enabled = true;
            }
            stunCountdown = stunTimer;
        }
    }

    public void TakeDamage(int amount)
    {
        soundManager.GetComponent<SoundManager>().takeDamagePlay();
        damage = true;
        currentHealth -= amount;
        GameObject hitmarker = Instantiate(playerhit, transform.position + new Vector3(0.0f,0.1f, 0.0f), Quaternion.identity) as GameObject;
        Destroy(hitmarker, 0.2f);

        // Stun code
        animator.SetBool("isStunned", true);
        isStunned = true;
        stunCountdown = stunTimer;

        // Disable walking and punching when stunned
        GetComponent<Movement>().enabled = false;

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

        animator.SetBool("idle2", false);
        animator.SetBool("isDead", true);

        if (player.transform.localScale.x >= 0)
        {
            player.transform.Rotate(0, 0, 90);
        }
        else if (player.transform.localScale.x <= 0)
        {
            player.transform.Rotate(0, 0, -90);
        }
        transform.localPosition = new Vector3(transform.localPosition.x, 0.253f, 7.68f);
    }
}
