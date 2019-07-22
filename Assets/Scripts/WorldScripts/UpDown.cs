using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDown : MonoBehaviour
{
    public bool goUp;
    Vector3 MaxPos;
    float speed = 0.1f;

    private void Start()
    {
        goUp = true;
        MaxPos = transform.position;
    }

    void Update()
    {
        if(goUp == true)
        {
            Up();
        } else
        {
            Down();
        }
    }

    void Up()
    {
        transform.Translate(0, Time.deltaTime * speed, 0);
        
        if(transform.position.y >= MaxPos.y + 0.1f)
        {
            goUp = false;
        }
    }

    void Down()
    {
        transform.Translate(0, -Time.deltaTime * speed, 0);

        if (transform.position.y <= MaxPos.y - 0.1f)
        {
            goUp = true;
        }
    }
}