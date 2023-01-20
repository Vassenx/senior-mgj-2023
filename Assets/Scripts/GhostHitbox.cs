using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostHitbox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.GhostHitPlayer(other.transform, transform.forward);
        }
    }
}
