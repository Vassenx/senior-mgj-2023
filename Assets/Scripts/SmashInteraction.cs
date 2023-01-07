using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashInteraction : MonoBehaviour
{
    [SerializeField] private Collider hitbox;
    [SerializeField] private Animator animator;
    
    public void ToggleHitbox(bool enabled) // Anim event
    {
        hitbox.enabled = enabled;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("SmashObject"))
            return;

        var smashable = other.GetComponent<Smashable>();
        if(smashable != null)
        {
            smashable.SmashObject();
        }
    }
    
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    animator.SetTrigger("Smash");
        //}
    }
}
