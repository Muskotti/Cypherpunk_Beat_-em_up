using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    GameObject player;
    public GameObject E;
    bool playerInRange;
    bool pressed;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = true;
            E.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
            E.SetActive(false);
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown("e") && pressed)
        {
            pressed = false;
            player.SendMessage("PickUp", "KeyCard");
            Destroy(this.gameObject);
        }
        else
        {
            pressed = true;
        }
    }

    public void SetLocation(Vector3 pos)
    {
        transform.position = new Vector3(pos.x, 0.2f, pos.z);
    }
}
