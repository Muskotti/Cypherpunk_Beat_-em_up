using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    public float stunTimer;
    public float stunCountdown;
    bool isStunned;

    GameObject soundManager;
    public Animator animator;

    public GameObject bloodSplatter;
    public GameObject enemyhit;
    public GameObject enemydead;

    public GameObject Credit;

    CharacterController cc;

    // Knockback
    float mass = 4.0F; // defines the character mass
    Vector3 impact = Vector3.zero;

    void Start()
    {
        health = 3;
        stunCountdown = 0;
        isStunned = false;
        soundManager = GameObject.Find("SoundManager");
        cc = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (isStunned)
        {
            stunCountdown = stunCountdown -= Time.deltaTime;
        }

        if (stunCountdown <= 0)
        {
            GetComponent<AIPath>().enabled = true;
            isStunned = false;
            stunCountdown = 0;
            animator.SetBool("isStunned", false);

            // Enable scripts after stun ends
            GetComponent<EnemyHit>().enabled = true;
            GetComponent<AIDestinationSetter>().EnableMovement();
        }

        // Knockback
        if (impact.magnitude > 0.2F && GetComponent<AIDestinationSetter>().canMove)
        {
            // consumes the impact energy each cycle:
            impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
        }
        //Gravity
        if (GetComponent<AIDestinationSetter>().canMove)
        {
            impact.y -= 20f * Time.deltaTime;
            cc.Move(impact * Time.deltaTime * 3);
        }

        // Death
        if (health <= 0 && stunCountdown <= 0)
        {
            SpawnCredit();
            Die();
        }
    }

    private void TakeDamage(String direction)
    {
        // Blood splatter on the ground
        GameObject splatter = Instantiate(bloodSplatter, transform.position + new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        splatter.transform.position = new Vector3(transform.position.x, 0.006f, transform.position.z - 0.5f);
        splatter.transform.Rotate(90, 0, 0);

        GetComponent<AIPath>().enabled = false;
        // Activate enemy pathfinding, if not yet active
        if (GetComponent<AIDestinationSetter>().canMove == false)
        {
            GetComponent<AIDestinationSetter>().canMove = true;
        }

        GetComponent<AIDestinationSetter>().stop = true;

        // Stun code
        animator.SetBool("isStunned", true);
        isStunned = true;
        stunCountdown = stunTimer;

        // Knockback enemy, if it's in the air
        if (impact.y > -3.8)
        {
            switch(direction)
            {
                case "up":
                    AddImpact(new Vector3(0, 0, 1), 30);
                    break;
                case "down":
                    AddImpact(new Vector3(0, 0, -1), 30);
                    break;
                case "left":
                    AddImpact(new Vector3(-1, 0, 0), 30);
                    break;
                case "right":
                    AddImpact(new Vector3(1, 0, 0), 30);
                    break;
            }
        }

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

        health -= 1;
    }

    private void TakeHeavyDamage(String direction)
    {
        TakeDamage(direction);
        AddImpact(new Vector3(0, -1, 0), 60);
        stunCountdown = 0.7f;
    }

    public void AddImpact(Vector3 dir, float force)
    {
        dir.Normalize();
        if (dir.y < 0)
        {
            dir.y = -dir.y; // reflect down force on the ground
        }
        impact += dir.normalized * force / mass;
    }

    private void SpawnCredit()
    {
        GameObject newBox = Instantiate(Credit);
        newBox.transform.position = new Vector3(transform.position.x, 0.2f, transform.position.z);
    }

    private void Die()
    {
        animator.SetTrigger("deathTrigger");
        animator.SetBool("isStunned", false);
        animator.SetBool("isWalking", false);

        MonoBehaviour[] comps = GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour c in comps)
        {
            c.enabled = false;
        }
        GetComponent<CharacterController>().enabled = false;

        GetComponent<SpriteRenderer>().enabled = true;

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        if (transform.localScale.x > 0)
        {
            transform.Rotate(0, 0, 90);
            transform.localPosition = new Vector3(transform.localPosition.x - 0.5f, transform.localPosition.y - 0.4f, transform.localPosition.z - 0.4f);
        }
        else if (transform.localScale.x < 0)
        {
            transform.Rotate(0, 0, -90);
            transform.localPosition = new Vector3(transform.localPosition.x + 0.5f, transform.localPosition.y - 0.4f, transform.localPosition.z - 0.4f);
        }
    }
}
