using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakOnContact : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    
    public void OnCollision()
    {
        Instantiate(prefab, transform.position, prefab.transform.rotation);
        Destroy(gameObject);
    }
}
