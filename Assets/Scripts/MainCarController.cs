using System;
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
            if (_canPlay)
            {
                rb.MoveRotation(transform.rotation * Quaternion.Euler(0f, Yaw * _rotationSpeed * Time.fixedDeltaTime, 0f));
                right.steerAngle = Mathf.Lerp(steeringLimit.x, steeringLimit.y, (1 + Yaw) / 2f);
                left.steerAngle = Mathf.Lerp(steeringLimit.x, steeringLimit.y, (1 + Yaw) / 2f);
            }
        }
        
        currentVelocity = Vector3.Lerp(currentVelocity, v, acceleration);
        rb.velocity = new Vector3(currentVelocity.x,rb.velocity.y,currentVelocity.z);
        engine.pitch = _animationCurve.Evaluate(rb.velocity.magnitude / max);
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
