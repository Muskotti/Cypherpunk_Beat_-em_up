using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyHit : MonoBehaviour
{
    public float hitCooldown;
    public float timeStamp;
    public float nextCooldown;

    GameObject player;
    GameObject enemy;
    public bool inPosition;

    public Animator animator;

    void Start()
    {
        enemy = gameObject;
        player = GameObject.Find("Player");
    }

    void Update()
    {
        timeStamp = Time.time;
        inPosition = enemy.GetComponent<Pathfinding.AIDestinationSetter>().inPosition;

        if (timeStamp >= nextCooldown && inPosition && player.GetComponent<PlayerHealt>().currentHealth > 0)
        {
            hitPlayer();
            nextCooldown = Time.time + hitCooldown;
        }

        // Toggle walk animation if enemy is not in position
        if (!inPosition && GetComponent<AIDestinationSetter>().canMove)
        {
            animator.SetBool("isWalking", true);
        } else
        {
            animator.SetBool("isWalking", false);
        }
    }

    void hitPlayer()
    {
        animator.SetTrigger("punchTrigger");
        player.GetComponent<PlayerHealt>().TakeDamage(1);

        if (gameObject.transform.parent.gameObject.name == "Soldiers")
        {
            player.GetComponent<PlayerHealt>().stunTimer = 0.5f;
            player.GetComponent<PlayerHealt>().stunCountdown = 0.5f;

            // Knockback player to direction the enemy is punching from (if player is not blocking)
            if (!player.GetComponent<Movement>().Block)
            {
                if (gameObject.transform.localScale.x < 0)
                {
                    player.GetComponent<PlayerHealt>().AddImpact(new Vector3(-3, -1, 0), 50);
                }
                else
                {
                    player.GetComponent<PlayerHealt>().AddImpact(new Vector3(3, -1, 0), 50);
                }
            }
            
        }
        else
        {
            player.GetComponent<PlayerHealt>().stunTimer = 1;
            player.GetComponent<PlayerHealt>().stunCountdown = 1;
        }
    }
}
