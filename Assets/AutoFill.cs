using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoFill : MonoBehaviour
{
    private Image fill;
    private float currentFill;
    private float time = 2f;

    private void Awake()
    {
        fill = GetComponent<Image>();
    }

    private void OnEnable()
    {
        currentFill = 0;
    }

    private void Update()
    {
        fill.fillAmount = currentFill;
        currentFill += Time.deltaTime * (1f/time);
    }
}
