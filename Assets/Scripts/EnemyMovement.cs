using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    GameObject player;


    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        moveTowardsPlayer();
    }

    public void moveTowardsPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, (Time.deltaTime));
    }
}
