using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenuScript : MonoBehaviour
{
    GameObject player;
    public GameObject upgradeMenuUI;
    public GameObject E;

    bool playerInRange;
    bool active;
    bool pressed;

    float speed = 0.1f;
    float height = 0.1f;

    private void Awake()
    {
        active = false;
        player = GameObject.FindGameObjectWithTag("Player");
        upgradeMenuUI.SetActive(false);
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
            activateMenu();
        } else
        {
            pressed = true;
        }
    }

    private void activateMenu()
    {
        active = !active;
        upgradeMenuUI.SetActive(active);
        player.SendMessage("SetMoveStatus", !active);
    }
}
