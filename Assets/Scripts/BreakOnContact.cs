using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BreakOnContact : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    
    public void OnCollision()
    {
        GameController.Instance.AddMoney(Random.Range(2,7));
        Instantiate(prefab, transform.position, prefab.transform.rotation);
        Destroy(gameObject);
    }
}
