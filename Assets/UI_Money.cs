using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Money : MonoBehaviour
{
    public GameObject addEffect;
    public Text text;
    int amount = 0;
    
    public void AddAmmount(int i)
    {
        Text t = Instantiate(addEffect, transform.parent).GetComponent<Text>();
        t.text = "+" + i;
        t.gameObject.SetActive(true);
        Destroy(t.gameObject, 1.5f);
        amount += i;
        text.text = "$" + amount;
    }
}
