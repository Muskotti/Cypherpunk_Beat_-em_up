using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paralaxScript : MonoBehaviour
{
    private float lenght, startposx, startposy;
    public GameObject camera;
    public float parallaxEffectX;
    public float parallaxEffectY;

    void Start()
    {
        startposx = transform.position.x;
        startposy = transform.position.y;
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {

        float temp = (camera.transform.position.x * (1 - parallaxEffectX));
        float disX = (camera.transform.position.x * parallaxEffectX);
        float disY = (camera.transform.position.y * parallaxEffectY);
        transform.position = new Vector3(startposx + disX, startposy + disY, transform.position.z);
        if(temp > startposx + lenght)
        {
            startposx += lenght;
        } else if(temp < startposx - lenght)
        {
            startposx -= lenght;
        }
    }
}
