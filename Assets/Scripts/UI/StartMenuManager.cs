using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuManager : MonoBehaviour
{
    [SerializeField] private string mainWorldName;
    [SerializeField] private GameObject settingsMenu;

    [SerializeField] private Slider mainAudioSlider;
    [SerializeField] private Slider musicAudioSlider;
    [SerializeField] private Slider soundEffectsAudioSlider;
    [SerializeField] private AudioMixer audioMixer;
	
    public void Start()
    {
        mainAudioSlider.onValueChanged.AddListener (delegate {MainAudioValueChange ();});
        musicAudioSlider.onValueChanged.AddListener (delegate {MusicAudioValueChange ();});
        soundEffectsAudioSlider.onValueChanged.AddListener (delegate {SoundEffectsAudioValueChange ();});
    }
    
    public void OnPlayGame() => SceneManager.LoadScene(mainWorldName);

    public void OnQuit() => Application.Quit();

    public void OnSettings() => settingsMenu.gameObject.SetActive(true);

    public void OnBack() => settingsMenu.gameObject.SetActive(false);

    public void MainAudioValueChange() => audioMixer.SetFloat ("MainVolume", Mathf.Log10(mainAudioSlider.value) * 20);
    public void MusicAudioValueChange() => audioMixer.SetFloat ("MusicVolume", Mathf.Log10(musicAudioSlider.value) * 20);
    public void SoundEffectsAudioValueChange() => audioMixer.SetFloat ("SoundEffectsVolume", Mathf.Log10(soundEffectsAudioSlider.value) * 20);
}
