using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuggerPostionMouse : MonoBehaviour
{
    [SerializeField] private RectTransform t;

    public Vector2 xMax;
    public Vector2 yMax;
    
    public void Pos(float x, float y)
    {
        t.anchoredPosition = new Vector3(Mathf.Lerp(xMax.x, xMax.y, x), Mathf.Lerp(yMax.x, yMax.y, y), 0);
    }
}
