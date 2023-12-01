using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject targetPosition;
    [SerializeField] private GameObject visuals;
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private float visualLerpSpeed = 0.5f;
    [SerializeField] private float camPosLerpSpeed = 0.5f;
    [SerializeField] private float camRotLerpSpeed = 0.5f;
    [SerializeField] private Vector3 viewBoundry = new Vector3(0.5f, 0.5f, 0);
    [SerializeField] private Vector3 rotationBoundry = new Vector3(10, 40, 0);
    [SerializeField] private Vector3 camPosBOundry = new Vector3(0.2f, 0.1f, 0);

    private Vector3 camInitialPos;
    private Vector3 initialPosition;
    private Vector3 targetPos = Vector3.zero;
    
    private void Start()
    {
        initialPosition = targetPosition.transform.localPosition;
        camInitialPos = cam.transform.localPosition;
    }

    private void Update()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        targetPos = targetPos + new Vector3(x, y, 0) * speed;

        targetPos.x = Mathf.Clamp(targetPos.x, -viewBoundry.x, viewBoundry.x);
        targetPos.y = Mathf.Clamp(targetPos.y, -viewBoundry.y, viewBoundry.y);
        
        targetPosition.transform.localPosition = initialPosition + targetPos;

        visuals.transform.localPosition = Vector3.Lerp(visuals.transform.localPosition, targetPosition.transform.localPosition, visualLerpSpeed);
        
        float tX = targetPos.x / viewBoundry.x;
        float tY = targetPos.y / viewBoundry.y;
        
        float pitch = Mathf.Lerp(-rotationBoundry.x, rotationBoundry.x, (tY + 1) / 2f );
        float yaw = Mathf.Lerp(-rotationBoundry.y, rotationBoundry.y, (tX + 1) / 2f);
        
        float offsetX = Mathf.Lerp(-camPosBOundry.x, camPosBOundry.x, (tX + 1) / 2f );
        float offsetY = Mathf.Lerp(-camPosBOundry.y, camPosBOundry.y, (tY + 1) / 2f);
        
        Debug.LogError(tX + " " + tY + "  "+ pitch +"  " + yaw);
        
        visuals.transform.localRotation = Quaternion.LookRotation(targetPosition.transform.localPosition - cam.transform.localPosition);
        cam.transform.localRotation = Quaternion.Lerp(cam.transform.localRotation, Quaternion.Euler(pitch, yaw,0), camRotLerpSpeed);
        cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, camInitialPos + new Vector3(offsetX, offsetY, 0), camPosLerpSpeed);
    }
}
