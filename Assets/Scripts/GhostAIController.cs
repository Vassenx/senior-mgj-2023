using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
public class GhostAIController : MonoBehaviour
{
    public NavMeshAgent agent;
    private Transform player;
    
    private bool isFloating = false;
    private Animator ghostAnimator;
    public void Init(Transform playerTransf)
    {
        player = playerTransf;
        ghostAnimator = GetComponent<Animator>();
    }
    
    private void Update()
    {
        if (agent.enabled)
        {
            agent.SetDestination(player.position);
        }
    }
    
    private void FixedUpdate()
    {
        while (isFloating) // lazy gravity
        {
            // slowly fall
            transform.position -= GhostSpawner.Instance.spawnOffset * 50 * Time.fixedDeltaTime;
        }
    }
    
    // this is so the ghost can rise from the vase before being put on the navmesh as navmesh agents can't go into the air
    public IEnumerator Float(GhostAIController newGhost)
    {
        agent.enabled = false;
        isFloating = true;
        yield return new WaitForSeconds(2);
        agent.enabled = true;
        isFloating = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ghostAnimator.SetTrigger("Attack");
            GameManager.Instance.GhostHitPlayer(player, collision.contacts[0].normal);
        }
    }
}