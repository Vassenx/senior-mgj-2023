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
    
    [SerializeField] private float knockbackForce;
    [SerializeField] private float delayBeforeCanBeHitAgain = 2f;
    private float timeSinceLastHit = 0;
    
    private int score = 0;
    
    private const string endScene = "LetterScene";
    
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
        timeSinceLastHit = Time.time;
    }

    public void GhostHitPlayer(Transform player, Vector3 directionHit)
    {
        if (Time.time - timeSinceLastHit > delayBeforeCanBeHitAgain)
        {
            health.LoseHealth();
            player.GetComponent<Rigidbody>().AddForce(knockbackForce * directionHit, ForceMode.Impulse);

            timeSinceLastHit = Time.time;
        }
    }
    
    public void Win() // TODO win
    {
        SceneManager.LoadScene(endScene);
    }

    public void Lose() // TODO die
    {
        SceneManager.LoadScene(endScene);
    }

    public void UpdateScore(int addedAmount)
    {
        score = addedAmount;
        scoreText.text = $"{score}";
    }
}