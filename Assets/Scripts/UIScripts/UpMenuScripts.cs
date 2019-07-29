using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpMenuScripts : MonoBehaviour
{
    public GameObject Player;
    public GameObject upgradeMenuUI;
    public GameObject punchPlus;
    public TextMeshProUGUI Credit;

    GameObject soundManager;

    GameObject thePlayer;
    Movement playerScript;

    private void Awake()
    {
        thePlayer = GameObject.Find("Player");
        playerScript = thePlayer.GetComponent<Movement>();
        soundManager = GameObject.Find("SoundManager");
    }

    public void FasterPunch()
    {
        if(playerScript.GetCredit() >= 5)
        {
            soundManager.GetComponent<SoundManager>().UpgradePlay();
            Player.SendMessage("UpgradePunch", true);
            punchPlus.SetActive(false);
            playerScript.SetCredits(5);
        }
    }

    public void MenuExit()
    {
        soundManager.GetComponent<SoundManager>().ButtonClickPlay();
        upgradeMenuUI.SetActive(false);
        Player.SendMessage("SetMoveStatus", true);
    }

    private void Update()
    {
        Credit.SetText("Credits " + playerScript.GetCredit());
    }
}
