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
    }

    void Update()
    {
        childs = transform.childCount;
        if (childs == 0 && send)
        {
            KeyCard.SendMessage("SetLocation", location);
            send = false;
        } else if(childs == 1)
        {
            SaveLocation();
        }
    }

    private void SaveLocation()
    {
        location = transform.GetChild(0).transform.position;
    }
}
