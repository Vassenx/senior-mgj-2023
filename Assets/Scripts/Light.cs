using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour
{
    private Vector3 startPos;
    
    [SerializeField] float offsetAmount;
    [SerializeField] float lerpSpeed;
    private float otheroffset;

    private void Start()
    {
        startPos = gameObject.transform.parent.transform.position;
        otheroffset = transform.position.y - startPos.y;
    }

    void Update()
    {
        var playerPos = gameObject.transform.parent.transform.position;
        transform.position = new Vector3(playerPos.x, playerPos.y + otheroffset + (offsetAmount * Mathf.Cos(lerpSpeed * Time.time)), playerPos.z);
    }
}
