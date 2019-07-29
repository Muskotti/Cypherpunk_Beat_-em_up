using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpriteLoader : MonoBehaviour
{
    public Sprite[] sprites;
    int random;

    // Start is called before the first frame update
    void Start()
    {
        random = Random.Range(1, 4);

        switch(random)
        {
            case 1:
                this.GetComponent<SpriteRenderer>().sprite = sprites[0];
                break;
            case 2:
                this.GetComponent<SpriteRenderer>().sprite = sprites[1];
                break;
            case 3:
                this.GetComponent<SpriteRenderer>().sprite = sprites[2];
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
