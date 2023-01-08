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

    [SerializeField] private float knockbackForce;
    [SerializeField] private float delayBeforeCanBeHitAgain = 2f;
    private float timeSinceLastHit = 0;
    
    private int score = 0;
    private int maxPointsPossible;
    
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
        scoreText.text = $"{score}";
        timeSinceLastHit = Time.time;
        maxPointsPossible = GameObject.FindObjectsOfType<Smashable>().Length;
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
    
    public void TryWin() // TODO win
    {
        if (score >= maxPointsPossible)
        {
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
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(endScene);
    }

    public void UpdateScore(int addedAmount)
    {
        score += addedAmount;
        scoreText.text = $"{score}";
    }
}