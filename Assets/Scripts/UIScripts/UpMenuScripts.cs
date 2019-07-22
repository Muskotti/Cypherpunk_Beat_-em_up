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

    GameObject thePlayer;
    Movement playerScript;

    private void Awake()
    {
        thePlayer = GameObject.Find("Player");
        playerScript = thePlayer.GetComponent<Movement>();
    }

    public void FasterPunch()
    {
        Player.SendMessage("UpgradePunch", true);
        punchPlus.SetActive(false);
    }

    public void MenuExit()
    {
        upgradeMenuUI.SetActive(false);
        Player.SendMessage("SetMoveStatus", true);
    }

    private void Update()
    {
        Credit.SetText("Credits " + playerScript.GetCredit());
    }
}
