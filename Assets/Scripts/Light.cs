using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour
{
    private Vector3 startPos;
    
    [SerializeField] float offsetAmount;
    [SerializeField] float lerpSpeed;

    private void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, startPos.y + (offsetAmount * Mathf.Cos(lerpSpeed * Time.time)), transform.position.z);
    }
}
