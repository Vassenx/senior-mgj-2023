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
    
    [SerializeField] private AudioSource smashedAudioSource;
    [SerializeField] private List<AudioClip> smashedClips;

    private void Start()
    {
        trashInside = trashPickupOptions[UnityEngine.Random.Range(0, trashPickupOptions.Count)];
    }

    public void SmashObject()
    {
        var curTransf = transform;
        Instantiate(destroyableObject, curTransf.position, curTransf.rotation, curTransf.parent);
        Destroy(gameObject);

        smashedAudioSource.clip = smashedClips[UnityEngine.Random.Range(0, smashedClips.Count)];
        smashedAudioSource.Play();
        
        GhostSpawner.Instance.SpawnGhost(curTransf);
        
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
