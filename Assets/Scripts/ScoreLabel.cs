using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreLabel : MonoBehaviour
{
    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
        text.text = "$" + PlayerPrefs.GetFloat("Score");
    }
}
