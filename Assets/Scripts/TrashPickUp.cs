using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class TrashPickUp : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    
    public void Spawn()
    {
        rb.AddForce(new Vector3(Random.Range(0.1f,0.5f), 1.5f, Random.Range(0.1f,0.5f)));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.UpdateScore(1);
            Destroy(this);
        }
    }
}
