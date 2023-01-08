using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashAudio : MonoBehaviour
{
    [SerializeField] private AudioSource smashedAudioSource;
    [SerializeField] private List<AudioClip> smashedClips;
    
    void Start()
    {
        smashedAudioSource.clip = smashedClips[UnityEngine.Random.Range(0, smashedClips.Count)];
        smashedAudioSource.Play();
    }
}
