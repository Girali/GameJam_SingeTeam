using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class MainCarController : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 currentVelocity;
    [SerializeField] private float acceleration = 0.05f;
    // Public fields

    [SerializeField] private float _speed = 50f;
    [SerializeField] private float _rotationSpeed = 270f;
    private bool _canPlay;

    [SerializeField] private WheelCollider left;
    [SerializeField] private WheelCollider right;
    [SerializeField, MinMaxSlider(-40,40)] private Vector2 steeringLimit = new Vector2(-10, 10);
    
    public float Yaw { get; set; } = 0f;
    public CarRagdoll carRagdoll;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        _canPlay = false;
        StartCoroutine(lockControl());
        gravityFrame = Physics.gravity.y * Time.fixedDeltaTime;
    }

    private float gravityFrame = 0;
    private float gravity = 0;
    
    private void Update()
    {
        Vector3 v = Vector3.zero;

        if (Input.GetKey(KeyCode.Space))
        {
            v = Vector3.Scale(transform.forward, new Vector3(1,0,1)) * _speed;
            if (_canPlay)
            {
                rb.MoveRotation(transform.rotation * Quaternion.Euler(0f, Yaw * _rotationSpeed * Time.deltaTime, 0f));
                right.steerAngle = Mathf.Lerp(steeringLimit.x, steeringLimit.y, (1 + Yaw) / 2f);
                left.steerAngle = Mathf.Lerp(steeringLimit.x, steeringLimit.y, (1 + Yaw) / 2f);
            }
        }
        
        currentVelocity = Vector3.Lerp(currentVelocity, v, acceleration);
        rb.velocity = new Vector3(currentVelocity.x,rb.velocity.y,currentVelocity.z);
    }

    private void OnCollisionStay(Collision other)
    {
        gravity = 0;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Environment"))
        {
            if (rb.velocity.magnitude > 3f)
            {
                carRagdoll.ActivateRagdoll();
            }
        }
    }

    private IEnumerator lockControl()
    {
        yield return new WaitForSeconds(2f);
        _canPlay = true;
    }
}
