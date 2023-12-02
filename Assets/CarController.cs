using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField, MinMaxSlider(-360, 360)]
    private Vector2 yawLimit;
    [SerializeField, MinMaxSlider(-360, 360)]
    private Vector2 pitchLimit;
    [SerializeField, MinMaxSlider(-360, 360)]
    private Vector2 tiltLimit;


    public void UpdateCarTilt(float x, float y, float z)
    {
        float yaw = Mathf.Lerp(yawLimit.x, yawLimit.y, y);
        float pitch = Mathf.Lerp(pitchLimit.x, pitchLimit.y, x);
        float tilit = Mathf.Lerp(tiltLimit.x, tiltLimit.y, z);

        transform.localRotation = Quaternion.Euler(pitch, yaw, tilit);
    }

    private void Turn90Left()
    {
    }
    
    private void Turn90Right()
    {
    }
}
