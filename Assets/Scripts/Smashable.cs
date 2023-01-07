using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smashable : MonoBehaviour
{
    [SerializeField] private GameObject destroyableObject;
    
    public void SmashObject()
    {
        var curTransf = transform;
        Instantiate(destroyableObject, curTransf.position, curTransf.rotation, curTransf.parent);
        Destroy(gameObject);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SmashObject();
        }
    }
}
