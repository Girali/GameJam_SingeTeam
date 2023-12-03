using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventRandomizer : MonoBehaviour
{
    
    [SerializeField]
    private float MinDelay = 2;
    [SerializeField]
    private float MaxDelay = 3;

    [SerializeField] private UnityEvent OnRandomInterval;

    private bool isActive = false;

    public void Activate()
    {
        isActive = true;
    }

    public void Deactivate()
    {
        isActive = false;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Randomise());
    }

    IEnumerator Randomise()
    {
        while (true)
        {
            yield return new WaitForSeconds(Mathf.Max(0, Random.Range(MinDelay, MaxDelay)));
            if (isActive)
            {
                OnRandomInterval.Invoke();
            }
        }
    }
}
