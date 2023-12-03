using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] public MainCarController mainCarController;
    
    [SerializeField, MinMaxSlider(-90, 90)]
    private Vector2 yawLimit;
    [SerializeField, MinMaxSlider(-90, 90)]
    private Vector2 pitchLimit;
    [SerializeField, MinMaxSlider(-90, 90)]
    private Vector2 tiltLimit;

    [SerializeField] private Animator _animator;
    [SerializeField] private Transform playerPos;
    [SerializeField] private Transform carRender;

    [SerializeField] private GameObject steeringWheel;

    public Transform headPosition;
    public AudioSourceRandomizer humanSound;
    
    private Vector3 rotAngle;
    public Transform PlayerPos
    {
        get => playerPos;
    }
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        rotAngle = steeringWheel.transform.right;
    }

    public void Updatebaba(float x)
    {
        float t = (x + 1) / 2f;
        //UpdateCarTilt(t, t, 0);
        steeringWheel.transform.Rotate(-rotAngle,x);
    }

    public void UpdateCarTilt(float x, float y, float z)
    {
        float yaw = Mathf.Lerp(yawLimit.x, yawLimit.y, y);
        float pitch = Mathf.Lerp(pitchLimit.x, pitchLimit.y, z);
        float tilit = Mathf.Lerp(tiltLimit.y, tiltLimit.x, x);

        carRender.localRotation = Quaternion.Euler(pitch, yaw, tilit);
    }

    private void Turn90Left()
    {
        _animator.SetTrigger("Left");
    }
    
    private void Turn90Right()
    {
        _animator.SetTrigger("Right");
    }
}
