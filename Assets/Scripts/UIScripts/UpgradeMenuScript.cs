using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenuScript : MonoBehaviour
{
    GameObject player;
    GameObject upgradeMenuUI;
    bool playerInRange;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        upgradeMenuUI = GameObject.FindGameObjectWithTag("UpgradeMenu");
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
        if (playerInRange)
        {
            activateMenu();
        }
    }

    private void activateMenu()
    {
        upgradeMenuUI.SetActive(false);
    }
}
