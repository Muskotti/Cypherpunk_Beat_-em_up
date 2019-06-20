using UnityEngine;
using System.Collections;
using TMPro;

public class FontScript : MonoBehaviour
{ 
    private TextMeshProUGUI textmeshPro;

    void Awake()
    {
        textmeshPro = GetComponent<TextMeshProUGUI>();
    }

    public void SetColorEnter()
    {
        textmeshPro.color = new Color32(200,200,200,255);
    }

    public void SetColorExit()
    {
        textmeshPro.color = new Color32(255, 255, 255, 255);
    }
}