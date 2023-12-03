using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class LocationRandomizer : MonoBehaviour
{
    [SerializeField] private Transform RandomizedObject;
    private List<Transform> RandomPlaces = new List<Transform>();
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform place in transform.GetComponentsInChildren<Transform>())
        {
            if(place.CompareTag("Lootzone"))
                RandomPlaces.Add(place);
        }
        
        Debug.Log($"{this.gameObject.name} has {RandomPlaces.Count} possible positions");
        
        if (RandomPlaces.Count == 0)
            return;
        
        int randIndex = Mathf.Clamp(Random.Range(0, RandomPlaces.Count + 1), 0, RandomPlaces.Count - 1);
        Debug.Log($"{this.gameObject.name} chose index {randIndex}");
        RandomizedObject.transform.position = RandomPlaces[randIndex].position;
    }

}
