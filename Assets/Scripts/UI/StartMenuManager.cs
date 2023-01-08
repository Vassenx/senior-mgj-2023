using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenu;

    [SerializeField] private Slider mainAudioSlider;
    [SerializeField] private Slider musicAudioSlider;
    [SerializeField] private Slider soundEffectsAudioSlider;
    [SerializeField] private AudioMixer audioMixer;

    [Header("Flags for Scenes")]
    [SerializeField] private bool isPauseMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private bool isLetterScene;
    
    private const string startWorldName = "StartScene";
    private const string mainWorldName = "GameScene";
    
    public InputSystemUIInputModule inputModule ;
    [SerializeField] private PlayerInput input;
    
    
    public void Start()
    {
        //Check if letter scene, if not, call the delegates
        if (!isLetterScene)
        {
            mainAudioSlider.onValueChanged.AddListener (delegate {MainAudioValueChange ();});
            musicAudioSlider.onValueChanged.AddListener (delegate {MusicAudioValueChange ();});
            soundEffectsAudioSlider.onValueChanged.AddListener (delegate {SoundEffectsAudioValueChange ();});   
        }
    }

    public void TogglePause(bool paused)
    {
        Time.timeScale = paused ? 0f : 1f;
    }
    
    private void OnEnable ()
    {
        //input.SwitchCurrentActionMap("UI");
        if(isPauseMenu)
            inputModule.cancel.action.performed += Escape;
    }

    private void OnDisable()
    {
        //input.SwitchCurrentActionMap("Player");
        if(isPauseMenu)
            inputModule.cancel.action.performed -= Escape;
    }

    public void OnPlayGame()
    {
        if (isPauseMenu)
        {
            TogglePause(false);
            pauseMenu.SetActive(false);
            settingsMenu.SetActive(false);
        }
        else
        {
            SceneManager.LoadScene(mainWorldName);
        }
    }

    public void OnQuit()
    {
        if (isPauseMenu)
        {
            TogglePause(false);
            SceneManager.LoadScene(startWorldName);
        }
        else
        {
            Application.Quit();
        }
    }

    //called for letter scene
    public void LoadGameLevel( AudioSource s)
    {
        StartCoroutine(WaitForAudioToLoad(s, "GameScene"));
    }

    public void LoadStartMenu(AudioSource s)
    {
        StartCoroutine(WaitForAudioToLoad(s, "StartScene"));
    }

    public void OnSettings() => settingsMenu.gameObject.SetActive(true);

    public void OnBack() => settingsMenu.gameObject.SetActive(false);

    public void MainAudioValueChange() => audioMixer.SetFloat ("MainVolume", Mathf.Log10(mainAudioSlider.value) * 20);
    public void MusicAudioValueChange() => audioMixer.SetFloat ("MusicVolume", Mathf.Log10(musicAudioSlider.value) * 20);
    public void SoundEffectsAudioValueChange() => audioMixer.SetFloat ("SoundEffectsVolume", Mathf.Log10(soundEffectsAudioSlider.value) * 20);
    
    public void Escape(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isPauseMenu)
            {
                TogglePause(!pauseMenu.activeInHierarchy);
                pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
                settingsMenu.SetActive(false);
            }
        }
    }
    
    
    /* Coroutine */
    IEnumerator WaitForAudioToLoad(AudioSource s, string scene)
    {
        yield return new WaitUntil(() => !s.isPlaying);
        SceneManager.LoadScene(scene);
        yield return null;
    }
}
