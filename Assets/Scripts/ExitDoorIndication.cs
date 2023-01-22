using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoorIndication : MonoBehaviour
{
    // on the doors you want to show as an exit (usually not this door)
    [SerializeField] private List<MeshRenderer> exitArrows;
    private float startArrowHeight;
    [SerializeField] private float floatingSpeed;
    [SerializeField] private float floatingAmplitude;
    
    private static event Action<ExitDoorIndication> OnEnterRoom; // parameter = door being entered

    private void Start()
    {
        OnEnterRoom += RemoveOtherRoomIndicators;
        
       startArrowHeight = transform.position.y;
    }

    private void Update()
    {
        if (!GameManager.Instance.NeedToFindExit())
            return;
        
        // making the arrow float up and down a bit
        var newPosY = startArrowHeight + floatingAmplitude * Mathf.Cos(Time.time*floatingSpeed);
        transform.position = new Vector3(transform.position.x,
                                                    newPosY,
                                                    transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!GameManager.Instance.NeedToFindExit())
            return;
        
        if (other.gameObject.CompareTag("Player"))
        {
            if (OnEnterRoom != null) // clear other indicators
            {
                OnEnterRoom.Invoke(this);
            }
            
            ToggleIndicators(true);
        }
    }

    private void RemoveOtherRoomIndicators(ExitDoorIndication curDoor)
    {
        if (curDoor == this)
            return;

        ToggleIndicators(false);
    }

    private void ToggleIndicators(bool enable)
    {
        foreach(var exitArrow in exitArrows)
        {
            exitArrow.enabled = enable;
        }
    }
}
