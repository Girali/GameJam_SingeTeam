using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI_Controller : MonoBehaviour
{
    private static GUI_Controller _instance;

    public static GUI_Controller Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<GUI_Controller>();
            return _instance;
        }
    }
}
