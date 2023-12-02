using System;
using UnityEngine;

public class Lootable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameController.Instance.AddLoot();
        }
    }
}
