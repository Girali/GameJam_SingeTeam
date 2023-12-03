using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternatePlay : MonoBehaviour
{

    public ParticleSystem p1;
    public ParticleSystem p2;
    public float cycle = 0.2f;
    IEnumerator Start()
    {
        while (true)
        {
            p1.Clear();
            p1.Play();
            yield return new WaitForSeconds(cycle);
            p2.Clear();
            p2.Play();
            yield return new WaitForSeconds(cycle);
        }
    }
}
