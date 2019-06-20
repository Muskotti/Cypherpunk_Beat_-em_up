using UnityEngine;
using TMPro;

public class FontScriptRed : MonoBehaviour
{ 
    private TextMeshProUGUI TextmeshPro;

    void Awake()
    {
        TextmeshPro = GetComponent<TextMeshProUGUI>();
    }

    public void SetColorEnter()
    {
        TextmeshPro.color = new Color32(166,0,0,255);
    }

    public void SetColorExit()
    {
        TextmeshPro.color = new Color32(255, 0, 0, 255);
    }
}