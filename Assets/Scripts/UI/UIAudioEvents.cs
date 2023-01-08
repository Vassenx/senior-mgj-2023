using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class UIAudioEvents : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<SoundEffects> audioClips;


    IEnumerator PlayWithDelay(AudioSource s)
    {
        if (!s.isPlaying)
        {
            //play with delay to avoid sound spamming
            s.Play(); 
            yield return new WaitForSeconds(1f);
            yield return null;
        }
    }
    /* Events */
    public void PlayAudio(SoundEffects sfx)
    {
        foreach (var sound in audioClips)
        {
            if (audioClips.Contains(sound))
            {
                AudioClip audioToPlay = sfx.audioClip;
                audioSource.clip = audioToPlay;
                StartCoroutine(PlayWithDelay(audioSource));
            }
            else
            {
                Debug.Log("audio does not exist");
            }
        }
    }
    
    
}
