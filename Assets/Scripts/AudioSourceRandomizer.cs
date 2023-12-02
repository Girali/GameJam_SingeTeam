using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Sirenix.OdinInspector;

public class AudioSourceRandomizer : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField, MinMaxSlider(0.2f,2f)] private Vector2 pitch = new Vector2(0.8f,1.2f);
    [SerializeField, MinMaxSlider(0.2f,2f)] private Vector2 volume = new Vector2(0.8f,1.2f);
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.pitch = Random.Range(pitch.x, pitch.y);
        _audioSource.volume = Random.Range(volume.x, volume.y);
        
        if(audioClips != null && audioClips.Length != 0)
            _audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
    }
}
