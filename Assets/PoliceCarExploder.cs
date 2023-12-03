using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceCarExploder : Shotable
{
    [SerializeField] private GameObject prefabExplod;
    [SerializeField] private GameObject impact;
    [SerializeField] private GameObject target;
    private int currentLife = 4;

    public override bool Shoted(Vector3 pos, Vector3 normal)
    {
        Instantiate(impact, pos, Quaternion.LookRotation(normal));

        currentLife--;

        if (currentLife < 0)
        {
            Instantiate(prefabExplod, transform.position, prefabExplod.transform.rotation);
            Destroy(target);
        }
        
        return true;
    }
}
