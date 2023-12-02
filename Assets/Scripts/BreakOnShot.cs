using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BreakOnShot : Shotable
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject impactPrefab;
    
    public override void Shoted(Vector3 pos, Vector3 normal)
    {
        GameObject.Instantiate(prefab, pos, Quaternion.identity);
        GameObject.Instantiate(prefab, pos, Quaternion.LookRotation(normal));
        Destroy(gameObject);
    }
}
