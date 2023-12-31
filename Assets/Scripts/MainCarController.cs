﻿using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class MainCarController : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 currentVelocity;
    [FoldoutGroup("CarMove")]
    [SerializeField] private float acceleration = 0.05f;
    // Public fields
    
    [FoldoutGroup("CarMove")]
    [SerializeField] private float _speed = 50f;
    [FoldoutGroup("CarMove")]
    [SerializeField] private float _rotationSpeed = 270f;
    [SerializeField] private float lateralRotation = 10f;
    private bool _canPlay;

    [FoldoutGroup("CarMove")]
    [SerializeField] private WheelCollider left;
    [FoldoutGroup("CarMove")]
    [SerializeField] private WheelCollider right;
    [FoldoutGroup("CarMove")]
    [SerializeField, MinMaxSlider(-40,40)] private Vector2 steeringLimit = new Vector2(-10, 10);

    [FoldoutGroup("VFX")]
    [SerializeField] private AudioSource engine;
    [FoldoutGroup("VFX")]
    [SerializeField] private float max;
    [FoldoutGroup("VFX")]
    [SerializeField] private AnimationCurve _animationCurve;
    public float Yaw { get; set; } = 0f;
    [FoldoutGroup("VFX")]
    public CarRagdoll carRagdoll;

    [FoldoutGroup("NPC Params")] [SerializeField]
    private float stressByShot;
    
    [FoldoutGroup("NPC Params")] [SerializeField]
    private float stressBySlap;
    
    [FoldoutGroup("NPC Params")] [SerializeField]
    private float passiveStressDowning;

    [FoldoutGroup("NPC Params")] [SerializeField]
    private float speedStress;
    
    [FoldoutGroup("NPC Params")] [SerializeField]
    private float minimumStress;

    private bool carMove = false;
    
    private float currentStress;
    private float targetStress;

    private float cdUnstuck = 3f;
    private float timerUnstuck = 0;
    private bool stuck = false;
    
    public void AddStress()
    {
        targetStress += stressByShot;
        targetStress = Mathf.Clamp(targetStress, minimumStress, 100f);
    }
    
    public void SubStress()
    {
        targetStress += stressBySlap;
        targetStress = Mathf.Clamp(targetStress, minimumStress, 100f);
    }

    private void Update()
    {
        targetStress -= passiveStressDowning * Time.deltaTime;
        currentStress = Mathf.Lerp(currentStress, targetStress, speedStress);
        currentStress = Mathf.Clamp(currentStress, minimumStress, 100f);
        GUI_Controller.Instance.stressBar.UpdateView(currentStress / 100f);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _canPlay = false;
        StartCoroutine(lockControl());
        gravityFrame = Physics.gravity.y * Time.fixedDeltaTime;
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(3f);
        yield return new WaitUntil(() => GameController.Instance.carStart);
        yield return new WaitForSeconds(1f);

        carMove = true;
    }

    private float gravityFrame = 0;
    private float gravity = 0;
    
    private void FixedUpdate()
    {
        Vector3 v = Vector3.zero;   

        if (carMove)
        {
            v = Vector3.Scale(transform.forward, new Vector3(1,0,1)) * _speed * (currentStress / 100f);
            right.motorTorque =(currentStress / 100f) * _speed * 10;
            left.motorTorque =(currentStress / 100f) * _speed * 10;
            
            if (_canPlay)
            {
                rb.MoveRotation(transform.rotation * Quaternion.Euler(0f, Yaw * _rotationSpeed * Time.fixedDeltaTime, 0f));
                right.steerAngle = Mathf.Lerp(steeringLimit.x, steeringLimit.y, (1 + Yaw) / 2f);
                left.steerAngle = Mathf.Lerp(steeringLimit.x, steeringLimit.y, (1 + Yaw) / 2f);
                
                v += Vector3.Scale(transform.right, new Vector3(1,0,1)) * Yaw * lateralRotation * Time.fixedDeltaTime;
            }
        }
        
        currentVelocity = Vector3.Lerp(currentVelocity, v, acceleration);
        rb.velocity = new Vector3(currentVelocity.x,rb.velocity.y,currentVelocity.z);
        engine.pitch = _animationCurve.Evaluate(rb.velocity.magnitude / max);
        
        MusicManager.Instance._epicness = currentStress / 100f;

        float f = Vector3.SignedAngle(Vector3.up, transform.up, transform.forward);

        rb.AddTorque(0,0,f * -1.5f);

        if (!stuck && Mathf.Abs(f) > 160)
        {
            timerUnstuck = Time.time;
            stuck = true;
        }
        else if (stuck && Mathf.Abs(f) < 160)
        {
            stuck = false;
        }
        else if (stuck && timerUnstuck + cdUnstuck < Time.time)
        {
            transform.position = transform.position + (Vector3.up * 2);
            transform.Rotate(0,0,180,Space.Self);
            stuck = false;
        }
        
        Debug.DrawRay(transform.position, rb.velocity.normalized * 50, Color.red);
        Debug.DrawRay(transform.position, rb.angularVelocity.normalized * 50, Color.magenta);
    }

    private void OnCollisionStay(Collision other)
    {
        gravity = 0;
    }

    private void OnCollisionEnter(Collision other)
    {
        BreakOnContact breakOnContact = other.collider.GetComponent<BreakOnContact>();
        if (breakOnContact)
        {
            breakOnContact.OnCollision();
            return;
        }
        
        if (other.gameObject.CompareTag("Environment"))
        {
            if (rb.velocity.magnitude > 10f)
            {
                carRagdoll.ActivateRagdoll();
                GameController.Instance.GameOver();
            }
        }
    }

    private IEnumerator lockControl()
    {
        yield return new WaitForSeconds(2f);
        _canPlay = true;
    }
}
