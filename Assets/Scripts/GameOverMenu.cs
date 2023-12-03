using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
    }

    private void Awake()
    {
        GUI_Controller.Instance.fadeInCallbackEvent += LoadScene;
        GUI_Controller.Instance.FadeOut();
        GameController.SetLockCursor(false);
    }

    public void Play()
    {
        GUI_Controller.Instance.FadeIn();
    }

    private void LoadScene()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
