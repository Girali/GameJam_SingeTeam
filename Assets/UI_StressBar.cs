using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_StressBar : MonoBehaviour
{
    [SerializeField] private Image fill;
    [SerializeField] private Gradient fillGrad;

    public void UpdateView(float t)
    {
        fill.fillAmount = t;
        fill.color = fillGrad.Evaluate(t);
    }
}
