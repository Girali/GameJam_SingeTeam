using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class PoliceCarController : Shotable
{
    [SerializeField] private GameObject player;
    private Rigidbody rb;

    [SerializeField] private float speed = 10f;
    [SerializeField] private float _rotationSpeed = 40f;
    
    [SerializeField] private AudioSource enigne;
    [SerializeField] private AudioSource siren;
    [SerializeField] private AnimationCurve _animationCurve;

    [SerializeField] private WheelCollider right;
    [SerializeField] private WheelCollider left;

    [SerializeField, MinMaxSlider(-40,40)] private Vector2 maxSteeringAngle;

    [SerializeField] private float maxDistanceDisapwn;
    
    public void Init(GameObject p)
    {
        player = p;
    }
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(10f, 20f));

            siren.Play();
            yield return new WaitForSeconds(Random.Range(40f, 60f));
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        BreakOnContact breakOnContact = other.collider.GetComponent<BreakOnContact>();
        if (breakOnContact)
        {
            breakOnContact.OnCollision();
        }
    }

    void FixedUpdate()
    {
        if (player)
        {
            Vector3 dir = Vector3.Scale(player.transform.position - transform.position, new Vector3(1,0,1)).normalized;

            Vector3 toplayerdir = Vector3.Scale(dir, new Vector3(1, 0, 1)); 
            Vector3 policeDir = Vector3.Scale(transform.forward, new Vector3(1, 0, 1)); 
            
            float a = Vector3.SignedAngle(-toplayerdir.normalized,policeDir.normalized, Vector3.up);

            float t = Mathf.Clamp(a / 45f, -1, 1);

            left.steerAngle = -Mathf.Lerp(maxSteeringAngle.x, maxSteeringAngle.y, (1+t) /2f);
            right.steerAngle = -Mathf.Lerp(maxSteeringAngle.x, maxSteeringAngle.y, (1+t) /2f);

            rb.MoveRotation(transform.rotation * Quaternion.Euler(0f, t * _rotationSpeed * Time.fixedDeltaTime, 0f));
            rb.velocity = new Vector3(dir.x * speed, rb.velocity.y, dir.z * speed);
        }

        enigne.pitch = _animationCurve.Evaluate(rb.velocity.magnitude / 10f);
    }

    private void Update()
    {
        if (player)
        {
            float dist = Vector3.Distance(transform.position, player.transform.position);

            if (dist > maxDistanceDisapwn)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        PoliceController.Instance.CarDestoryed();
    }
}
