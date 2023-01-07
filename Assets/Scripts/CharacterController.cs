using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float playerSpeed;
    [SerializeField] private float airSpeed;
    [SerializeField] private Vector2 moveInput;

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
    }
    
    void Update()
    {
        CheckGrounded();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        if (isMoving && isGrounded)
        {
            rb.velocity = new Vector3(moveInput.x * playerSpeed * Time.fixedDeltaTime, rb.velocity.y, moveInput.y * playerSpeed * Time.fixedDeltaTime);
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
