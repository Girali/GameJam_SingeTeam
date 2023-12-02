using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CarRagdoll : MonoBehaviour
{
    [SerializeField] private Collider[] _colliders;
    
    public void ActivateRagdoll()
    {
        foreach (var c in _colliders)
        {
            if (c != null)
            {
                c.gameObject.AddComponent<Rigidbody>();
                c.transform.parent = null;
            }
        }
    }
}
