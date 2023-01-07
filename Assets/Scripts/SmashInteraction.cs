using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    public void OnSmash(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            animator.SetTrigger("Smash");
            context.action.Reset();
        }
    }
}
