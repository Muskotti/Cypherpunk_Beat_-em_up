using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    float health;

    void Start()
    {
        health = 5;
    }

    private void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0) {
            Destroy(this.gameObject);
        }
    }
}
