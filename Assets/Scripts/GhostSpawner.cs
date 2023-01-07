using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    [SerializeField] private GhostAIController ghost;
    
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
        Instantiate(ghost, spawnTransform.position, spawnTransform.rotation);
        // TODO: ghost and vase don't collide
    }
}
