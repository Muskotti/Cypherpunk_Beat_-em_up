using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditPickUp : MonoBehaviour
{
    GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            player.SendMessage("PickUp", "Credit");
            Destroy(this.gameObject);
        }
    }
}
