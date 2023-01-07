using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float playerSpeed;
    [SerializeField] private float airSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private Vector2 moveInput;
    [SerializeField] private float playerTurnSpeed;

    [Header("Jump Attributes")] 
    [SerializeField] private bool isGrounded;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask layersThatGrounds;

    [SerializeField] private float groundCheckOffset;
    [SerializeField] private float colliderCheckRad;

    private Rigidbody rb;
    private CapsuleCollider playerCollider;
    private bool isMoving;
    private Animator animator;
    //used to orient player based on camera rotation
    private Transform camera;
    [SerializeField] private Vector3 targetDir;
    
    void Awake()
    {
        //call scripts here
    }

    void Start()
    {
        //other components here
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
        camera = Camera.main.transform;
    }
    
    void Update()
    {
        CheckGrounded();
    }

    private void FixedUpdate()
    {
        Movement();
        TurnPlayer();
    }

    void Movement()
    {
        targetDir = Vector3.zero;
        targetDir = camera.forward * moveInput.y;
        targetDir += camera.right * moveInput.x;
        if (isMoving && isGrounded)
        {
            rb.velocity = new Vector3(targetDir.x * playerSpeed * Time.fixedDeltaTime, rb.velocity.y, targetDir.z * playerSpeed * Time.fixedDeltaTime);

        }
        //on air
        else if (!isGrounded)
        {
            rb.velocity = new Vector3(moveInput.x * airSpeed * Time.fixedDeltaTime, rb.velocity.y, moveInput.y * airSpeed * Time.fixedDeltaTime);
        }
        
        animator.SetFloat("Speed", rb.velocity.sqrMagnitude);
    }

    void TurnPlayer()
    {
        Vector3 targetDir = Vector3.zero;
        targetDir = camera.forward * moveInput.y;
        targetDir = targetDir + camera.right * moveInput.x;
        targetDir.y = 0;
        targetDir.Normalize();

        if (targetDir == Vector3.zero) targetDir = transform.forward;

        //looking towards the target direction
        Quaternion targetRot = Quaternion.LookRotation(targetDir);
        //lerps to the rotation target
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRot, playerTurnSpeed * Time.deltaTime);
        transform.rotation = playerRotation;
    }
    

    void CheckGrounded()
    {
        Vector3 castPos = new Vector3(gameObject.transform.position.x,
            gameObject.transform.position.y - groundCheckOffset,
            gameObject.transform.position.z);
        if (Physics.CheckSphere(castPos, colliderCheckRad, layersThatGrounds, QueryTriggerInteraction.Ignore)) 
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    void Jump()
    {
        rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        animator.SetTrigger("Jump");
    }

    /* Events */
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        isMoving = moveInput != Vector2.zero;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            Jump();
        }
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 castPos = new Vector3(gameObject.transform.position.x,
            gameObject.transform.position.y - groundCheckOffset,
            gameObject.transform.position.z);
        Gizmos.DrawSphere(castPos, colliderCheckRad);
    }
    
    
}