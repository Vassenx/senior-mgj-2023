using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class IntroSceneManager : MonoBehaviour
{
    private const string startWorldName = "StartScene";
    [SerializeField] private float timeUntilStartNewScene = 10f;
    
    private void Start()
    {
        StartCoroutine(SwitchSceneAfterTime(timeUntilStartNewScene));
    }

    public void Escape(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SceneManager.LoadScene(startWorldName);
        }
    }

    IEnumerator SwitchSceneAfterTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(startWorldName);
    }
}
