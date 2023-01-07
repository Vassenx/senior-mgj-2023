using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollideMonkeyNoRender : MonoBehaviour
{
    [SerializeField] private GameObject sensor;
    [SerializeField] private List<GameObject> listOfColluded;
    private BoxCollider colliderChecker;
    
    // Start is called before the first frame update
    void Start()
    {
        colliderChecker = sensor.GetComponent<BoxCollider>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("wall"))
        {
            if (!listOfColluded.Exists(x => other.gameObject))
            {
                listOfColluded.Add(other.gameObject);
            }

            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (var toHide in listOfColluded)
        {
            toHide.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
