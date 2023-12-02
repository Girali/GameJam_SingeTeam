using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class GunController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    
    [SerializeField] private GameObject muzzleFlash;
    [SerializeField] private GameObject muzzleFlashPrefab;
    [SerializeField] private GameObject defaultImpact;
    [SerializeField] private Animator animator;

    private int layerMask;

    private void Awake()
    {
        layerMask = LayerMask.GetMask("Default");
    }

    public void Shot()
    {
        animator.SetTrigger("Shot");
        GameObject.Instantiate(muzzleFlashPrefab, muzzleFlash.transform.position, muzzleFlash.transform.rotation,muzzleFlash.transform);
        
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if(Physics.Raycast(ray, out hit, 100f, layerMask))
        {
            Shotable shotable =  hit.collider.GetComponent<Shotable>();
            if (shotable != null)
            {
                bool b = shotable.Shoted(hit.point, hit.normal);
                if (!b)
                    GameObject.Instantiate(defaultImpact, hit.point, Quaternion.LookRotation(hit.normal));
            }
            else
            {
                GameObject.Instantiate(defaultImpact, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
    }
}
