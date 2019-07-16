using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelScript : MonoBehaviour
{
    GameObject player;

    public GameObject E;

    bool playerInRange;

    public Movement playerKeyCard;

    public GameObject Dialock;

    public bool pressed;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pressed = true;
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
            pressed = true;
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown("e") && playerKeyCard.HasKeyCard)
        {
            SceneManager.LoadScene("SampleScene");
        }
        else if (playerInRange && Input.GetKeyDown("e") && pressed)
        {
            pressed = false;
            Dialock.SendMessage("SetTalking", "Hmm. It looks like i need a Keycard to leave this place");
        }
    }
}
