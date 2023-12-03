using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoToBaseText : MonoBehaviour
{
    [SerializeField] private List<Lootable> lootables;
    [SerializeField] private GameObject text;
    private bool trigger;

    private void Start()
    {
        trigger = false;
    }

    void Update()
    {
        int looted = 0;
        foreach (Lootable lootable in lootables)
        {
            Debug.Log("oui");
            if (lootable.Looted == true || lootable.gameObject.activeSelf == false)
                looted++;
        }
        if (looted == lootables.Count)
        {
            DisplayText();
        }

        if (trigger == true)
        {
            StartCoroutine(HideText());
        }
    }

    IEnumerator HideText()
    {
        yield return new WaitForSeconds(5);
        text.SetActive(false);
    }
    
    void DisplayText()
    {
        text.SetActive(true);
        trigger = true;
    }
}
