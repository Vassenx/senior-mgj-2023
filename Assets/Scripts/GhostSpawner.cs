using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    [SerializeField] private GhostAIController ghost;
    [SerializeField] private Transform player;
    public Vector3 spawnOffset = new Vector3(0,5,0);
    
#region Singleton
    private static GhostSpawner instance;
    public static GhostSpawner Instance { get { return instance; } }

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

    public void SpawnGhost(Transform spawnTransform)
    {
        var newGhost = Instantiate(ghost, spawnTransform.position + spawnOffset, spawnTransform.rotation);
        newGhost.Init(player);
        StartCoroutine(ghost.Float(newGhost));
        
        // TODO: ghost and vase don't collide
    }
}
