using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollideMonkeyNoRender : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    
    private void Update()
    {
        transform.position = cameraTransform.position; // hack: wacky Cinemachine issues
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("wall"))
        {
            var meshRend = other.gameObject.GetComponent<MeshRenderer>();
            if (meshRend != null)
            {
                meshRend.enabled = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("wall"))
        {
            var meshRend = other.gameObject.GetComponent<MeshRenderer>();
            if (meshRend != null)
            {
                meshRend.enabled = true;
            }
        }
    }
}
