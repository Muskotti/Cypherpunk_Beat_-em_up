﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogScript : MonoBehaviour
{
    GameObject player;
    GameObject dialogScreen;

    bool playerInRange;

    public string text;

    bool pressed;

    private void Awake()
    {
        pressed = false;
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
            pressed = !pressed;
        }

        if(pressed)
        {
            Interact();
        }
    }

    private void Interact()
    {
        dialogScreen.SendMessage("SetTalking", text);
    }
}
