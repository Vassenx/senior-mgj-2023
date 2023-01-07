using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.AI;

public class GhostAIController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    private Transform player;

    public void Init(Transform playerTransf)
    {
        player = playerTransf;
    }
    
    private void Update()
    {
        agent.SetDestination(player.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.GhostHitPlayer();
        }
    }
}