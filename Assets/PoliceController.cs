using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class PoliceController : MonoBehaviour
{
    private static PoliceController _instance;

    public static PoliceController Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<PoliceController>();
            return _instance;
        }
    }
    
    [SerializeField]private GameObject prefabPolice;

    [SerializeField] private GameObject player;
    [SerializeField, MinMaxSlider(0, 30)] private Vector2 spawnRange = new Vector2(20,30);

    private int layerRaycast;
    private int spawnablelayer;

    private int spawnedCar;
    private int toSpawnCount = 1;
    
    private void Start()
    {
        spawnablelayer = LayerMask.GetMask("Water");
        layerRaycast = LayerMask.GetMask("Default", "Water");
    }

    public void CarDestoryed()
    {
        spawnedCar--;
    }
    
    private bool SpawnPoliceCar()
    {
        Vector3 posSpawn = transform.position + new Vector3(Random.Range(-spawnRange.y, spawnRange.y), 250, Random.Range(-spawnRange.y, spawnRange.y));

        Debug.DrawRay(posSpawn,Vector3.down * 300, Color.red );

        float dist = Vector3.Distance(transform.position, posSpawn);

        if (dist < spawnRange.x)
            return false;
        
        RaycastHit hit;

        if (Physics.Raycast( posSpawn, Vector3.down, out hit,300, layerRaycast))
        {
            if (hit.collider.gameObject.name.Contains("PoliceSpawnArea"))
            {
                GameObject g = GameObject.Instantiate(prefabPolice,
                    hit.point + (Vector3.up * 2), Quaternion.LookRotation(hit.point - player.transform.position));
                g.GetComponent<PoliceCarController>().Init(player);
                spawnedCar++;
                return true;
            }
        }

        return false;
    } 
    
    private void Update()
    {
        toSpawnCount = GameController.Instance.pointsDone;

        if (spawnedCar < toSpawnCount)
        {
            SpawnPoliceCar();
        }

        transform.position = player.transform.position;
    }
}
