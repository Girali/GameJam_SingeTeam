using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArrowGUI : MonoBehaviour
{
    private Lootable[] points;
    private VictoryScreen victoryTrigger;
    public MeshRenderer arrow;
    private void Awake()
    {
        points = FindObjectsByType<Lootable>(FindObjectsSortMode.None);
        victoryTrigger = FindObjectOfType<VictoryScreen>();
    }

    private bool toVictory = false;

    private void Update()
    {
        Lootable[] ltbs = points.Where(e => !e.Looted).OrderBy(e => Vector3.Distance(transform.position, e.transform.position)).ToArray();
        if (ltbs.Length == 0)
        {
            if (toVictory == false)
            {
                toVictory = true;
                arrow.materials[0].SetColor("FresnelColor", Color.green);
            }

            transform.rotation = Quaternion.Lerp(transform.rotation,
                Quaternion.LookRotation(transform.position - victoryTrigger.transform.position), 0.03f);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(transform.position - ltbs[0].transform.position),  0.03f );
        }
    }
}
