using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Stars : MonoBehaviour
{
    [SerializeField]private Image[] stars;
    [SerializeField]private Sprite emptyStar;
    [SerializeField]private Sprite fullStar;

    public void UpdateView(int i)
    {
        for (int j = 0; j < stars.Length; j++)
        {
            if (i > j)
            {
                stars[j].sprite = fullStar;
                stars[j].GetComponent<Jun_TweenRuntime>().Play();
                stars[j].color = Color.white;
            }
            else
            {
                stars[j].sprite = emptyStar;
                stars[j].GetComponent<Jun_TweenRuntime>().StopPlay();
                stars[j].color = Color.white;
            }
        }
    }
}
