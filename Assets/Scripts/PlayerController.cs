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

    private GunController gunController;
    private CarController carController;
    
    private Vector3 camInitialPos;
    private Vector3 initialPosition;
    private Vector3 targetPos = Vector3.zero;
    private int layerMask;

    private void Awake()
    {
        layerMask = LayerMask.GetMask("Default");
        gunController = GetComponent<GunController>();
        SetCar(FindObjectOfType<CarController>());
    }

    public void SetCar(CarController car)
    {
        carController = car;

        transform.position = carController.PlayerPos.position;
        transform.parent = carController.PlayerPos;
        initialPosition = targetPosition.transform.localPosition;
        camInitialPos = cam.transform.localPosition;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gunController.Shot();
            if(carController)
                carController.mainCarController.AddStress();
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            if(carController)
                carController.mainCarController.SubStress();
        }
        
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        targetPos = targetPos + new Vector3(x, y, 0) * speed;

        targetPos.x = Mathf.Clamp(targetPos.x, -viewBoundry.x, viewBoundry.x);
        targetPos.y = Mathf.Clamp(targetPos.y, -viewBoundry.y, viewBoundry.y);
        
        targetPosition.transform.localPosition = initialPosition + targetPos;

        visuals.transform.localPosition = Vector3.Lerp(visuals.transform.localPosition, targetPosition.transform.localPosition, visualLerpSpeed);
        
        float tX = targetPos.x / viewBoundry.x;
        float tY = targetPos.y / viewBoundry.y;
        
        if (carController && carController.mainCarController)
        {
            carController.mainCarController.Yaw = tX;
        }
        
        float pitch = Mathf.Lerp(rotationBoundry.x, -rotationBoundry.x, (tY + 1) / 2f );
        float yaw = Mathf.Lerp(-rotationBoundry.y, rotationBoundry.y, (tX + 1) / 2f);
        
        float offsetX = Mathf.Lerp(-camPosBOundry.x, camPosBOundry.x, (tX + 1) / 2f );
        float offsetY = Mathf.Lerp(-camPosBOundry.y, camPosBOundry.y, (tY + 1) / 2f);

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        Quaternion newRot = Quaternion.identity;
        
        if (Physics.Raycast(ray, out hit, 100f, layerMask))
        {
            newRot = Quaternion.LookRotation(hit.point - visuals.transform.position);
            Debug.DrawLine(hit.point , ray.origin , Color.green);
            
            Debug.DrawRay(visuals.transform.position, visuals.transform.position - hit.point);
        }
        else
        {
            newRot = Quaternion.LookRotation(targetPosition.transform.localPosition - cam.transform.localPosition);
        }

        if(carController)
            carController.Updatebaba(tX);
        
        visuals.transform.rotation = Quaternion.Lerp(visuals.transform.rotation, newRot, 0.02f);
        cam.transform.localRotation = Quaternion.Lerp(cam.transform.localRotation, Quaternion.Euler(pitch, yaw,0), camRotLerpSpeed);
        cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, camInitialPos + new Vector3(offsetX, offsetY, 0), camPosLerpSpeed);
    }
}
