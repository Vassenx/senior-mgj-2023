using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Smashable : MonoBehaviour
{
    [SerializeField] private GameObject destroyableObject;
    [SerializeField] private int destroyableScoreIncrease = 1;
    
    public void SmashObject()
    {
        var curTransf = transform;
        Instantiate(destroyableObject, curTransf.position, curTransf.rotation, curTransf.parent);
        Destroy(gameObject);

        GameManager.Instance.UpdateScore(destroyableScoreIncrease);
        
        GhostSpawner.Instance.SpawnGhost(curTransf);
    }
    
    private void Update()
    {
        if (Keyboard.current.fKey.isPressed)
        {
            SmashObject();
        }
    }
}