using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelScript : MonoBehaviour
{
    GameObject player;

    public GameObject E;

    bool playerInRange;

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
        if (playerInRange && Input.GetKeyDown("e"))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
