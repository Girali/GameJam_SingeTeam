using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainAudioSourceFader : MonoBehaviour
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

    private AudioSource _audioSource;
    [SerializeField] private float maxVolume;
    [SerializeField] private float fadeDuration = 4f;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void FadeOut()
    {
        StartCoroutine(CRT_Fade(true, fadeDuration));
    }
    
    private void FadeIn()
    {
        StartCoroutine(CRT_Fade(false, fadeDuration));
    }

    IEnumerator CRT_Fade(bool enabledVolume, float time)
    {
        float delta = 1f / time;

        float currentVolume = _audioSource.volume;

        if (enabledVolume)
        {
            _audioSource.Play();
            
            while (currentVolume < maxVolume)
            {
                currentVolume += Time.deltaTime * delta;
                _audioSource.volume = currentVolume;
                yield return new WaitForEndOfFrame();
            }

            _audioSource.volume = maxVolume;
        }
        else
        {
            while (currentVolume > 0)
            {
                currentVolume -= Time.deltaTime * delta;
                _audioSource.volume = currentVolume;
                yield return new WaitForEndOfFrame();
            }
            
            _audioSource.Stop();
            _audioSource.volume = 0;
        }
    }
}
