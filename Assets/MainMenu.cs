using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        GUI_Controller.Instance.FadeOut();
        MainAudioSourceFader.Instance.Play();
        volumeSlider.value = Volume;
    }

    public static float Volume
    {
        get
        {
            return PlayerPrefs.GetFloat("Volume",0.5f);
        }

        set
        {
            PlayerPrefs.SetFloat("Volume", value);
        }
    }

    public void Play()
    {
        MainAudioSourceFader.Instance.Stop();
        GUI_Controller.Instance.FadeIn();
        GUI_Controller.Instance.fadeInCallbackEvent += LoadScene;
    }

    void LoadScene()
    {
        string s = PlayerPrefs.GetString("LastPlayed","Tutorial");
        SceneManager.LoadScene(s);
    }

    [SerializeField] private UnityEngine.UI.Slider volumeSlider;
    
    public void SetVolume()
    {
        Volume = volumeSlider.value;
        AudioListener.volume = volumeSlider.value;
    }
    
    
    public void Quit()
    {
      Application.Quit();  
    }
}
