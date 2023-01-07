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

    public void LoseHealth(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            hearts[i].SetActive(false);
            health--;
            if (health <= 0)
            {
                health = 0;
                break;
            }
        }

        if (health <= 0)
        {
            GameManager.Instance.Lose();
        }
    }
}