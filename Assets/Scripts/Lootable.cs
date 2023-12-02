using System;
using UnityEngine;

public class Lootable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other == playerCarCollider)
        {
            gameController.AddLoot();
        }
    }

    // Private fields
    [SerializeField] private GameController gameController;
    [SerializeField] private BoxCollider playerCarCollider;
}
