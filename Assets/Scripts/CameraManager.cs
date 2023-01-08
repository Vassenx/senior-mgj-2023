using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Vector2 mouseInput;
    [SerializeField] private float camSpeed;

    [SerializeField] private float camYaw;
    [SerializeField] private float camPitch;
    [SerializeField] private float maxAngle;
    [SerializeField] private float minAngle;
    [SerializeField] private float offset;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        RotateCamera();
    }

    void RotateCamera()
    {
        camYaw = ClampAngle(camYaw, float.MinValue, float.MaxValue);
        camPitch = ClampAngle(camPitch, this.minAngle, this.maxAngle);
        player.transform.rotation = Quaternion.Euler(camPitch + offset, camYaw, 0.0f);
    }
    
    /* Events */
    public void OnCameraRotate(InputAction.CallbackContext context)
    {
        mouseInput = context.ReadValue<Vector2>();
        if (mouseInput.magnitude >= 0.01f)
        {
            camYaw += mouseInput.x * Time.deltaTime * camSpeed;
            camPitch -= mouseInput.y * Time.deltaTime * camSpeed;
        }
    }
    
    private float ClampAngle(float angle, float minAngle, float maxAngle)
    {
        if (angle < -360f) angle += 360f;
        if (angle > 360f) angle -= 360f;
        return Mathf.Clamp(angle, minAngle, maxAngle);
    }
}
