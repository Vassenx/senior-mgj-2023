using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private List<GameObject> hearts;
    [SerializeField] private int health;
    
    private void Start()
    {
        foreach (var heart in hearts)
        {
            heart.SetActive(true);
        }

        health = hearts.Count;
    }

    public void LoseHealth()
    {
        health--;
        hearts[health].SetActive(false);

        if (health > 0)
        {
            GameManager.Instance.Hurt();
        }

        else if (health <= 0)
        {
            health = 0;
            GameManager.Instance.isGameOver = true;
            GameManager.Instance.Lose();
        }
    }
}