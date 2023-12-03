using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CarRagdoll : MonoBehaviour
{
    private List<Collider> _colliders;
    private GameObject _player;
    [SerializeField] private GameObject explosion;
    
    public void Start()
    {
        _colliders = FindObjectsCollidersByTag("CarParts");
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    public void ActivateRagdoll()
    {
        GameObject.Instantiate(explosion, transform.position + (Vector3.up * 0.5f), explosion.transform.rotation);
        _player.GetComponent<SphereCollider>().isTrigger = false;
        _player.AddComponent<Rigidbody>();
        
        foreach (var collider in _colliders)
        {
            if (collider != null)
            {
                collider.gameObject.AddComponent<Rigidbody>();
                collider.transform.parent = null;
            }
        }
    }
    
    private List<Collider> FindObjectsCollidersByTag(string tag)
    {
        List<Collider> colliders = new List<Collider>();

        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);

        foreach (GameObject obj in objectsWithTag)
        {
            Collider collider = obj.GetComponent<Collider>();
            if (collider != null)
            {
                colliders.Add(collider);
            }
        }

        return colliders;
    }
}
