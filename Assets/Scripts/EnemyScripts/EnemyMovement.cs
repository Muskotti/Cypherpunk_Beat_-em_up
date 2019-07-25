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
            if (transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
            inPosition = false;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x + 0.9f, player.transform.position.y, player.transform.position.z), (Time.deltaTime));
        }
        else if (player.transform.position.x > this.transform.position.x)
        {
            if (transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
            inPosition = false;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x - 0.9f, player.transform.position.y, player.transform.position.z), (Time.deltaTime));
        }

        if (((transform.position.x < player.transform.position.x && transform.position.x > player.transform.position.x - 1f) || (transform.position.x > player.transform.position.x && transform.position.x < player.transform.position.x + 1f)) &&
            (transform.position.z == player.transform.position.z))
        {
            inPosition = true;
        }
    }
}
