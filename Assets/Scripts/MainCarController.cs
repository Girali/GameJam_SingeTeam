using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCarController : MonoBehaviour
{
    private void Start()
    {
        _canPlay = false;
        StartCoroutine(lockControl());
    }
    
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _rigidbody.MovePosition(transform.position + (transform.forward * (_speed * Time.deltaTime)));
            if (_canPlay)
            {
                _rigidbody.MoveRotation(transform.rotation * Quaternion.Euler(0f, Yaw * _rotationSpeed * Time.deltaTime, 0f));
            }
        }
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private IEnumerator lockControl()
    {
        yield return new WaitForSeconds(2f);
        _canPlay = true;
    }

    // Public fields
    public float Yaw { get; set; } = 0f;

    // Private fields
    private Rigidbody _rigidbody;
    private float _speed = 50f;
    private readonly float _rotationSpeed = 270f;
    private bool _canPlay;
}
