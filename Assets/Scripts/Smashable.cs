using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Smashable : MonoBehaviour
{
    [SerializeField] private GameObject destroyableObject;
    private TrashPickUp trashInside;
    [SerializeField] private List<TrashPickUp> trashPickupOptions;

    private void Start()
    {
        trashInside = trashPickupOptions[UnityEngine.Random.Range(0, trashPickupOptions.Count)];
    }

    public void SmashObject()
    {
        var curTransf = transform;
        var newVase = Instantiate(destroyableObject, curTransf.position, curTransf.rotation, curTransf.parent);
        GameManager.Instance.Smashed(newVase);
        Destroy(gameObject);

        if (CompareTag("Ghostable"))
        {
            GhostSpawner.Instance.SpawnGhost(curTransf);

            for (int i = 0; i < 2; i++)
            {
                var pos = new Vector3(transform.position.x + 0.1f * i, transform.position.y, transform.position.z + 0.1f * i);
                var extraTrash = Instantiate(trashInside, pos, trashInside.transform.rotation);
                extraTrash.Spawn(); 
            }
        }
        
        var trash = Instantiate(trashInside, transform.position, trashInside.transform.rotation);
        trash.Spawn();
    }
    
    private void Update()
    {
        if (Keyboard.current.fKey.isPressed)
        {
            SmashObject();
        }
    }
}
