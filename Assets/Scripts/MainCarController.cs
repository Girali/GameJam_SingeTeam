using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCarController : MonoBehaviour
{
    void Update()
    {
        transform.Translate(Vector3.forward * (_speed * Time.deltaTime));
        transform.Rotate(Vector3.up, Yaw * _rotationSpeed * Time.deltaTime);
    }

    // Public fields
    public float Yaw { get; set; } = 0f;

    // Private fields
    private float _speed = 10f;
    private readonly float _rotationSpeed = 90f;
}
