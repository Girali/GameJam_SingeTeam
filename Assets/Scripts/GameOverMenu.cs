using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private Text playText;
    public bool vitroryScreen;
    public void Quit()
    {
        Application.Quit();
    }

    private void Awake()
    {
        AudioListener.volume = MainMenu.Volume;

        GUI_Controller.Instance.fadeInCallbackEvent += LoadScene;
        GUI_Controller.Instance.FadeOut();
        GameController.SetLockCursor(false);
        MainAudioSourceFader.Instance.Play();
        if (vitroryScreen)
        {
            string s = PlayerPrefs.GetString("LastPlayed");
            if (s == "Ville")
            {
                playText.text = "PLAY AGAIN";
            }
            else
            {
                playText.text = "NEXT MAP";
                PlayerPrefs.SetString("LastPlayed","Ville");
            }
        }
    }

    public void Play()
    {
        GUI_Controller.Instance.FadeIn();
        MainAudioSourceFader.Instance.Stop();
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("LastPlayed"));
    }
}
