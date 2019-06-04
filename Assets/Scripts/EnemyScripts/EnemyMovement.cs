using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    GameObject player;
    public bool inPosition;

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
        if (player.transform.position.x < this.transform.position.x)
        {
            inPosition = false;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x + 0.8f, player.transform.position.y, player.transform.position.z), (Time.deltaTime));
        }
        else if (player.transform.position.x > this.transform.position.x)
        {
            inPosition = false;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x - 0.8f, player.transform.position.y, player.transform.position.z), (Time.deltaTime));
        }

        if (((transform.position.x < player.transform.position.x && transform.position.x > player.transform.position.x - 0.9f) || (transform.position.x > player.transform.position.x && transform.position.x < player.transform.position.x + 0.9f)) &&
            (transform.position.z == player.transform.position.z))
        {
            inPosition = true;
        }
    }
}
