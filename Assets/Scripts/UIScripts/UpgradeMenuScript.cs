using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenuScript : MonoBehaviour
{
    GameObject player;
    GameObject upgradeMenuUI;

    bool playerInRange;
    bool active;
    bool pressed;

    public Movement playerMovement;

    private void Awake()
    {
        active = false;
        player = GameObject.FindGameObjectWithTag("Player");
        upgradeMenuUI = GameObject.FindGameObjectWithTag("UpgradeMenu");
        upgradeMenuUI.SetActive(active);
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
        playerMovement.SetMoveStatus(!active);
    }
}
