using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableCursor : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
