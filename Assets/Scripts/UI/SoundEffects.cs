using UnityEngine;

[CreateAssetMenu(fileName = "AudioBit", menuName = "Create Sounds/Add Sound", order = 1)]
public class SoundEffects : ScriptableObject
{
    public string audioName;
    public AudioClip audioClip;
}
