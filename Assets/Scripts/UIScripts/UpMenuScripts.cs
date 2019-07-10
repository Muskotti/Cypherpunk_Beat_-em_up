using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpMenuScripts : MonoBehaviour
{
    public GameObject Player;
    public GameObject upgradeMenuUI;
    public GameObject punchPlus;

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
}
