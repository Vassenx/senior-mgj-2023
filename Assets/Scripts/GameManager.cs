using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private StartMenuManager startMenuManager;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Health health;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private CharacterController playerController;
    
    [SerializeField] private float knockbackForce;
    [SerializeField] private float delayBeforeCanBeHitAgain = 2f;
    private float timeSinceLastHit = 0;
    
    private int score = 0;
    [SerializeField] private int maxPointsPossible = 30;
    
    private const string endScene = "LetterScene";
    private bool hasWon = false;
    public bool HasWon
    {
        get { return hasWon;  }
    }

    private int previousGhostCount = 0;
    [SerializeField] private AudioSource backgroundMusicAudioSource;
    [SerializeField] private List<AudioClip> backgroundMusicLeastIntenseFirst;
    
#region Singleton
    private static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
    }

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(this);
    }
#endregion

    private void Awake()
    {
        score = 0;
        scoreText.text = $"{score}";
        timeSinceLastHit = Time.time;
        // just in case one disappears, we preset max to lower value
        // maxPointsPossible = GameObject.FindObjectsOfType<Smashable>().Length;
        
        backgroundMusicAudioSource.clip = backgroundMusicLeastIntenseFirst[0];
        backgroundMusicAudioSource.Play();
    }

    private void Update()
    {
        var ghostCount = GhostSpawner.Instance.ghosts.Count;
        
        // im tired
        if (previousGhostCount < 3 && ghostCount >= 3)
        {
            backgroundMusicAudioSource.clip = backgroundMusicLeastIntenseFirst[1];
            backgroundMusicAudioSource.Play();
        }
        else if (previousGhostCount < 6 && ghostCount >= 6)
        {
            backgroundMusicAudioSource.clip = backgroundMusicLeastIntenseFirst[2];
            backgroundMusicAudioSource.Play();
        }
        else if (previousGhostCount < 9 && ghostCount >= 9)
        {
            backgroundMusicAudioSource.clip = backgroundMusicLeastIntenseFirst[3];
            backgroundMusicAudioSource.Play();
        }
    }

    public void GhostHitPlayer(Transform player, Vector3 directionHit, GhostAIController ghost)
    {
        if (Time.time - timeSinceLastHit > delayBeforeCanBeHitAgain)
        {
            
            health.LoseHealth();
            player.GetComponent<Rigidbody>().AddForce(knockbackForce * directionHit, ForceMode.Impulse);

            timeSinceLastHit = Time.time;

            ghost.AttackAnim();
        }
    }
    
    public void TryWin() // TODO win
    {
        if (score >= maxPointsPossible)
        {
            hasWon = true;
            SceneManager.LoadScene(endScene);
        }
    }
    
    public void Hurt()
    {
        playerAnimator.SetTrigger("Hurt");
    }

    public void Lose() // TODO die
    {
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        playerAnimator.SetTrigger("Cower");
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene(endScene);
    }

    public void UpdateScore(int addedAmount)
    {
        score += addedAmount;
        scoreText.text = $"{score}";
        playerController.PlayScoreAudios();
    }

    public void Smashed(GameObject obj)
    {
        StartCoroutine(RemoveShards(obj));
    }
    
    private IEnumerator RemoveShards(GameObject obj)
    {
        yield return new WaitForSeconds(4f);
        Destroy(obj);
    }
}