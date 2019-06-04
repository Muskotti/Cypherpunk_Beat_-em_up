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
    bool inPosition;
    

    void Start()
    {
        enemy = GameObject.Find("Enemy");
        player = GameObject.Find("Player");
    }

    void Update()
    {
        timeStamp = Time.time;
        inPosition = enemy.GetComponent<EnemyMovement>().inPosition;

        if (timeStamp >= nextCooldown && inPosition)
        {
            hitPlayer();
            nextCooldown = Time.time + hitCooldown;
        }
    }

    void hitPlayer()
    {
        player.GetComponent<PlayerHealt>().TakeDamage(1);
    }
}
