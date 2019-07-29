using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCardScript : MonoBehaviour
{

    public int childs;
    public Vector3 location;
    public GameObject KeyCard;
    public bool send;

    private void Start()
    {
        send = true;
        childs = transform.childCount;
    }

    void Update()
    {
        if (childs == 0 && send)
        {
            KeyCard.SendMessage("SetLocation", location);
            send = false;
        }
    }

    public void SaveLocation(Vector3 l)
    {
        location = l;
    }

    public void DecreaseChildAmount()
    {
        childs--;
    }
}
