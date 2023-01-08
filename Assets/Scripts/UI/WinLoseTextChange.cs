using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinLoseTextChange : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI loseText;
    [SerializeField] private TextMeshProUGUI wonText;

    private void Update()
    {
        StartCoroutine(LetterChangeOnLoad());
    }

    IEnumerator LetterChangeOnLoad() // ignore this code, its hideous, thought there was an error
    {
        yield return new WaitForEndOfFrame();

        loseText.enabled = !GameManager.Instance.HasWon;
        wonText.enabled = GameManager.Instance.HasWon;
        
        Destroy(this);
    }
}
