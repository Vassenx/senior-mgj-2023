using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SmashInteraction : MonoBehaviour
{
    [SerializeField] private Animator animator;

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
}
