using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Smashable : MonoBehaviour
{
    [SerializeField] private GameObject destroyableObject;
    [SerializeField] private TrashPickUp trashInside;
    
    public void SmashObject()
    {
        var curTransf = transform;
        Instantiate(destroyableObject, curTransf.position, curTransf.rotation, curTransf.parent);
        Destroy(gameObject);

        GhostSpawner.Instance.SpawnGhost(curTransf);
    }
    
    private void Update()
    {
        if (Keyboard.current.fKey.isPressed)
        {
            SmashObject();
            var trash = Instantiate(trashInside, transform.position, trashInside.transform.rotation);
            trash.Spawn();
        }
    }
}
