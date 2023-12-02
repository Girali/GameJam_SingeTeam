using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotable : MonoBehaviour
{
    public virtual bool Shoted(Vector3 pos, Vector3 normal)
    {
        return false;
    }
}
