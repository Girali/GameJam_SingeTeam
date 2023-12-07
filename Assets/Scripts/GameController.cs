using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    public bool carStart = false;

    private void Awake()
    {
        GUI_Controller.Instance.FadeOut();
        GameController.SetLockCursor(true);
        AudioListener.volume = MainMenu.Volume;
        PlayerPrefs.SetFloat("Score", _score);
    }

    public void GameOver()
    {
        GUI_Controller.Instance.FadeIn();
        GUI_Controller.Instance.fadeInCallbackEvent += LoadGameOverScene;
    }

    void LoadGameOverScene()
    {
        SceneManager.LoadScene("LoseMenu");
    }

    public static void SetLockCursor(bool b)
    {
        Cursor.lockState = b ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !b;
    }

    public void AddMoney(int i)
    {
        _score += i;
        GUI_Controller.Instance.money.AddAmmount(i);
    }
    
    public void AddLoot()
    {
        pointsDone++;
        AddMoney(100);
        PlayerPrefs.SetFloat("Score", _score);
        GUI_Controller.Instance.stars.UpdateView(pointsDone);
    }

    public int pointsDone = 0;
    
    // Private fields
    [SerializeField] private int _score = 0;
}
