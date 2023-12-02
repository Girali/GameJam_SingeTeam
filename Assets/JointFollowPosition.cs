using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointFollowPosition : MonoBehaviour
{
    [SerializeField] private CharacterJoint _characterJoint;
    [SerializeField] private Transform pivot;

    private void Update()
    {
        _characterJoint.connectedAnchor = pivot.position;
    }
}
