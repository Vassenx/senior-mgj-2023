using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashInteraction : MonoBehaviour
{
    private Collider hitbox;


    private void ToggleHitbox(bool enabled)
    {
        hitbox.enabled = enabled;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("SmashObject"))
            return;
    }
}
