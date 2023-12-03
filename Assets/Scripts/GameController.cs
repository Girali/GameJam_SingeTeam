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

    private void Awake()
    {
        GUI_Controller.Instance.FadeOut();
        GameController.SetLockCursor(true);
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
    
    public void AddLoot()
    {
        pointsDone++;
        _score += 100;
        if (_scoreText)
        {
            _scoreText.text = "$" + _score;
        }
        
        GUI_Controller.Instance.stars.UpdateView(pointsDone);
        PlayerPrefs.SetFloat("Score", _score);
    }

    private int pointsDone = 0;
    
    // Private fields
    [SerializeField] private uint _score = 0;
    [SerializeField] private Text _scoreText;
}
