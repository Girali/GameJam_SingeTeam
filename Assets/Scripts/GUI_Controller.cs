using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI_Controller : MonoBehaviour
{
    private static GUI_Controller _instance;

    public static GUI_Controller Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<GUI_Controller>(); 
            return _instance;
        }
    }

    [SerializeField] private Jun_TweenRuntime fadeIn;
    [SerializeField] private Jun_TweenRuntime fadeOut;

    public delegate void UICallaback();
    public event UICallaback fadeInCallbackEvent;
    public event UICallaback fadeOutCallbackEvent;

    public void CallbackFadeIn(){fadeInCallbackEvent.Invoke();}
    public void CallbackFadeOut(){fadeOutCallbackEvent.Invoke();}
    
    public void FadeIn()
    {
        fadeIn.Play();
    }
    
    public void FadeOut()
    {
        fadeOut.Play();
    }
}
