using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private StartMenuManager startMenuManager;
    [SerializeField] private TextMeshProUGUI scoreText;
    
    private int score = 0;
    
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
    }
#endregion

    private void Awake()
    {
        score = 0;
    }

    public void GhostHitPlayer()
    {
        // TODO: die
        Time.timeScale = 0f;
    }

    public void UpdateScore(int addedAmount)
    {
        score = addedAmount;
        scoreText.text = $"Score: {score}";
    }
}