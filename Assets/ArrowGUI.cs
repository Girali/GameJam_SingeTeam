using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArrowGUI : MonoBehaviour
{
    private Lootable[] points;
    private void Awake()
    {
        points = FindObjectsByType<Lootable>(FindObjectsSortMode.None);
    }

    private void Update()
    {
        Lootable[] ltbs = points.Where(e => !e.Looted).OrderBy(e => Vector3.Distance(transform.position, e.transform.position)).ToArray();
        
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(transform.position - ltbs[0].transform.position),  0.01f );
    }
}
