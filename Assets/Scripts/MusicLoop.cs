using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MusicLoop", menuName = "ScriptableObjects/MusicLoop", order = 1)]
public class MusicLoop : ScriptableObject
{
    public List<AudioClip> clips;
}
