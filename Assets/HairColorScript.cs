using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairColorScript : MonoBehaviour
{
    private SpriteRenderer sprite;
    public PlayerHealt player;

    private void Awake()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        switch (player.currentHealth)
        {
            case 4:
                sprite.color = new Color(0.5f, 1f, 0, 1f);
                break;
            case 3:
                sprite.color = new Color(1f, 1f, 0, 1f);
                break;
            case 2:
                sprite.color = new Color(1f, 0.5f, 0, 1f);
                break;
            case 1:
                sprite.color = new Color(1f, 0, 0, 1f);
                break;
            case 0:
                sprite.color = new Color(1f, 0, 0, 1f);
                break;
            default:
                sprite.color = new Color(0, 1f, 0, 1f);
                break;
        }
    }
}
