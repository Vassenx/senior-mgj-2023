using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollideMonkeyNoRender : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("wall"))
        {
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("wall"))
        {
            other.gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
