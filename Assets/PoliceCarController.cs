using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PoliceCarController : Shotable
{
    private GameObject player;
    private Rigidbody rb;

    [SerializeField] private float speed = 10f;
    [SerializeField] private GameObject prefabExplod;
    [SerializeField] private GameObject impact;
    
    private int currentLife = 15;
    
    public virtual bool Shoted(Vector3 pos, Vector3 normal)
    {
        Instantiate(impact, pos, Quaternion.LookRotation(normal));

        currentLife--;

        if (currentLife < 0)
        {
            Instantiate(prefabExplod, transform.position, prefabExplod.transform.rotation);
            Destroy(gameObject);
        }
        
        return true;
    }
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 dir = (transform.position - player.transform.position).normalized * speed;

        
        rb.velocity = dir;
    }
}
