using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    void hitPlayer()
    {
        animator.SetTrigger("punchTrigger");
        player.GetComponent<PlayerHealt>().TakeDamage(1);
    }
}
