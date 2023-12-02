using System;
using UnityEngine;

public class Lootable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other == playerCarCollider)
        {
            GameController.Instance.AddLoot();
        }
    }

    // Private fields
    [SerializeField] private BoxCollider playerCarCollider;
}
