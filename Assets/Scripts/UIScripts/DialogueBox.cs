using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueBox : MonoBehaviour
{
    GameObject player;
    public GameObject dialogBox;
    public TextMeshProUGUI dialog;

    GameObject soundManager;

    bool Talking;
    
    void Awake()
    {
        Talking = true;
        player = GameObject.FindGameObjectWithTag("Player");
        soundManager = GameObject.Find("SoundManager");
    }
    
    void Update()
    {
        if (Input.GetKeyDown("e") && dialogBox.activeSelf)
        {
            soundManager.GetComponent<SoundManager>().ButtonClickPlay();
            player.SendMessage("SetMoveStatus", true);
            Talking = false;
            dialogBox.SetActive(false);
        } else if (Talking)
        {
            player.SendMessage("SetMoveStatus", false);
            dialogBox.SetActive(true);
        }
    }

    void SetTalking(string Text)
    {
        Talking = true;
        dialog.SetText(Text);
    }
}
