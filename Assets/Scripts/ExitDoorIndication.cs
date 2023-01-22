using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoorIndication : MonoBehaviour
{
    // on the doors you want to show as an exit (usually not this door)
    [SerializeField] private List<GameObject> exitArrows;
    private float startArrowHeight;
    private static float floatingSpeed;
    [SerializeField] private float floatingSpeedNotStatic;
    
    private static event Action<ExitDoorIndication> OnEnterRoom; // parameter = door being entered

    private void Start()
    {
        floatingSpeed = floatingSpeedNotStatic; // hack: rip no static in inspector
        
        OnEnterRoom += RemoveOtherRoomIndicators;
        
       startArrowHeight = transform.position.y;
    }

    private void Update()
    {
        // making the arrow float up and down a bit
        var newPosY = startArrowHeight + Mathf.Cos(Time.time*floatingSpeed);
        transform.position = new Vector3(transform.position.x,
                                                    newPosY,
                                                    transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
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
            exitArrow.SetActive(enable);
        }
    }
}
