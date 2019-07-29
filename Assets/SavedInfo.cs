using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedInfo : MonoBehaviour
{
    private static int credits;

    public static int Credits
    {
        get
        {
            return credits;
        }

        set
        {
            credits = value;
        }
    }
}
