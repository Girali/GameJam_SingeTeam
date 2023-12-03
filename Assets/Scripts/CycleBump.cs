using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CycleBump : MonoBehaviour
{

    [SerializeField] private Animator anim;
    [SerializeField, MinMaxSlider(1,10)] private Vector2 delay = new Vector2(1,3);
    
    IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(delay.x, delay.y));
            anim.SetTrigger("Bump");
        }
    }
}
