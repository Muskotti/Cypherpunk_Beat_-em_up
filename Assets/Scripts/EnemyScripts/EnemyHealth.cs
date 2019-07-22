using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class EnemyHealth : MonoBehaviour
{
    float health;
    public float stunTimer;
    public float stunCountdown;
    bool isStunned;

    GameObject soundManager;
    public Animator animator;

    public GameObject enemyhit;
    public GameObject enemydead;

    public GameObject Credit;

    void Start()
    {
        health = 3;
        stunCountdown = stunTimer;
        isStunned = false;
        soundManager = GameObject.Find("SoundManager");
    }

    private void Update()
    {
        if (isStunned)
        {
            stunCountdown = stunCountdown -= Time.deltaTime;
        }

        if (stunCountdown <= 0)
        {
            isStunned = false;
            stunCountdown = stunTimer;
            animator.SetBool("isStunned", false);

            // Enable scripts after stun ends
            GetComponent<EnemyHit>().enabled = true;
            GetComponent<AIDestinationSetter>().EnableMovement();
        }
    }

    private void TakeDamage(float damage)
    {
        // Activate enemy pathfinding, if not yet active
        if (GetComponent<AIDestinationSetter>().canMove == false)
        {
            GetComponent<AIDestinationSetter>().canMove = true;
        }

        // Stun code
        animator.SetBool("isStunned", true);
        isStunned = true;
        stunCountdown = stunTimer;

        // Disable walking and punching when stunned
        GetComponent<EnemyHit>().enabled = false;
        GetComponent<AIDestinationSetter>().DisableMovement();

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
            SpawnCredit();
            Destroy(this.gameObject);
        }
    }

    private void SpawnCredit()
    {
        GameObject newBox = Instantiate(Credit);
        newBox.transform.position = new Vector3(transform.position.x, 0.2f, transform.position.z);
    }
}
