using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollideMonkeyNoRender : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    private Vector3 cameraDirection;
    public float cameraDistance;

    private void Update()
    {
        cameraDirection = (cameraTransform.position - transform.parent.position);
        cameraDirection.y = 0;
        cameraDirection = (cameraDirection.normalized * cameraDistance) + transform.parent.position;
        transform.position = new Vector3(cameraDirection.x, 1, cameraDirection.z); // hack: wacky Cinemachine issues
        //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, cameraTransform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
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
