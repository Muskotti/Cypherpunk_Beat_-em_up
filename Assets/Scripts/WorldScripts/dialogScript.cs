using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogScript : MonoBehaviour
{
    GameObject player;
    GameObject dialogScreen;

    bool playerInRange;

    public string text;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        dialogScreen = GameObject.FindGameObjectWithTag("DialogScreen");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown("e"))
        {
            dialogScreen.SendMessage("SetTalking", text);
        }
    }
}
