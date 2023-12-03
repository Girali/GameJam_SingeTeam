using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

public class ReplaceThings : MonoBehaviour
{
    public ThingToReplace[] thingToReplaces;

    [Button("Replace NOW")]
    private void Replace()
    {
        StartCoroutine(CRT());
    }
    
    IEnumerator CRT()
    {
        GameObject parentObject = new GameObject("UrbanDetails");
        GameObject[] onMapObjects = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None);

        List<GameObject> toDelete = new List<GameObject>();
        
        foreach (ThingToReplace thing in thingToReplaces)
        {
            List<GameObject> objets = onMapObjects.Where(e => e != null).Where(e => e.name.Contains(thing.name))
                .ToList();
            
            yield return null;
            
            GameObject obj = new GameObject(thing.name.Replace("_","")+"Parent");
            obj.transform.parent = parentObject.transform;
            int i = 0;
            foreach (GameObject o in objets)
            {
                GameObject p = Instantiate(thing.prefab);
                
                p.name = thing.name + i;
                p.transform.position = o.transform.position;
                p.transform.rotation = o.transform.rotation;
                p.transform.localScale = o.transform.localScale;
                p.transform.parent = obj.transform;
                
                i++;
                toDelete.Add(o);
            }
        }

        GameObject DELETEPARENT = new GameObject("TODELETE");

        foreach (GameObject d in toDelete)
        {
            d.transform.parent = DELETEPARENT.transform;
        }
        
        DestroyImmediate(DELETEPARENT);
    }
}

[System.Serializable]
public class ThingToReplace
{
    public string name;
    public GameObject prefab;
}
