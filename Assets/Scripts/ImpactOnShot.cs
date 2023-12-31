using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactOnShot : Shotable
{
    [SerializeField] private GameObject prefab;

    public override bool Shoted(Vector3 pos, Vector3 normal)
    {
        GameObject.Instantiate(prefab, pos, Quaternion.LookRotation(normal));
        return true;
    }
}
