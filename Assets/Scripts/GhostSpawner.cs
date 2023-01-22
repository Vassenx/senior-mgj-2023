using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GhostSpawner : MonoBehaviour
{
    [SerializeField] private GhostAIController ghost;
    [SerializeField] private Transform player;
    public Vector3 spawnOffset = new Vector3(0,5,0);
    public float spawnDelay = 1f;
    public List<GhostAIController> ghosts;
    public GameObject spawnParticlePrefab;
    
    //delete me
    [SerializeField] private Transform spawner;
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

    private void Awake()
    {
        ghosts = new List<GhostAIController>();
    }

    public void SpawnGhost(Transform spawnTransform) {

        Vector3 position = spawnTransform.position;
        Quaternion rotation = spawnTransform.rotation;
        StartCoroutine(SpawnGhostCR(position, rotation));

    }

    private IEnumerator SpawnGhostCR(Vector3 position, Quaternion rotation)
    {
        print("IEnumarting woooo!!!!");
        // Spawn particles
        var spawnParticle = Instantiate(spawnParticlePrefab, position, rotation);
        Destroy(spawnParticle.gameObject, 3f);

        yield return new WaitForSeconds(spawnDelay);

        print("Spawnin ghost!!!");

        var newGhost = Instantiate(ghost, position + spawnOffset, rotation);
        newGhost.Init(player);
        ghosts.Add(newGhost);
        StartCoroutine(ghost.Float(newGhost));

        // TODO: ghost and vase don't collide
    }
    
    /* Test Event for debugging ghost ai */
    public void OnSpawn(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SpawnGhost(spawner);
        }
    }


}
