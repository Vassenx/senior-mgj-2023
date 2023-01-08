using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEscape : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.TryWin();
        }
    }
}
