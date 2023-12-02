using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController _instance;

    public static GameController Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<GameController>();
            return _instance;
        }
    }

    public void SetLockCursor(bool b)
    {
        Cursor.lockState = b ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !b;
    }
    
    public void AddLoot()
    {
        _score += 100;
    }
    
    // Private fields
    [SerializeField] private uint _score = 0;
}
