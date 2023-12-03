using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MusicManager : MonoBehaviour
{
    private static MusicManager _instance;

    public static MusicManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<MusicManager>();
            return _instance;
        }
    }

    private double _clipDuration;
    private double _goalTime;
    [SerializeField] private AudioSource[] _sources;
    private int _audioSourceToggle;
    private AudioClip _currentClip;

    [SerializeField] private List<MusicLoop> _loops;
    [Range(0,1)]
    public float _epicness;
    [SerializeField] private int _currentLoopIndex;
    private int _subLoopIndex;

    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private AudioLowPassFilter[] filters;


    private void Awake()
    {
        foreach (MusicLoop loop in _loops)
        {
            foreach (AudioClip clip in loop.clips)
            {
                clip.LoadAudioData();
            }
        }
    }

    void Start()
    {
        _goalTime = AudioSettings.dspTime;
    }

   

    private void Update()
    {
        SwitchTrack();
        if (AudioSettings.dspTime > _goalTime - 1)
        {
            PlayClip();
            PlayScheduledClip();
        }

        float _value = _curve.Evaluate(_epicness);
        foreach (AudioLowPassFilter filter in filters)
        {
            filter.cutoffFrequency = Mathf.Clamp((_value * 22000),2000, 22000);
        }
        
    }

    private void PlayScheduledClip()
    {
        _sources[_audioSourceToggle].clip = _currentClip;
        _sources[_audioSourceToggle].PlayScheduled(_goalTime);

        _clipDuration = (double)_currentClip.samples / _currentClip.frequency;
        _goalTime = _goalTime + _clipDuration;

        _audioSourceToggle = 1 - _audioSourceToggle;
    }

    private void SwitchTrack()
    {
        // Sampling epicness
        int _queuedLoopIndex = (int)Mathf.Round(_epicness * (_loops.Count - 1));
        if (_queuedLoopIndex != _currentLoopIndex)
        {
            _currentLoopIndex = _queuedLoopIndex;
            _subLoopIndex = 0;
        }
    }

    void PlayClip()
    {
        _currentClip = _loops[_currentLoopIndex].clips[_subLoopIndex];
        _subLoopIndex++;
        _subLoopIndex = _subLoopIndex % _loops[_currentLoopIndex].clips.Count;
    }

}
