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
    [SerializeField] private Animator animator;
    //used to orient player based on camera rotation
    private Transform camera;
    [SerializeField] private Vector3 targetDir;

    [SerializeField] private AudioSource trashPickUpAudioSource;
    [SerializeField] private AudioSource footStepsSource;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> trashPickupNoises;
    [SerializeField] private List<AudioClip> singingNoises;
    [SerializeField] private List<AudioClip> smashNoises;
    [SerializeField] private List<AudioClip> wowNoises;
    [SerializeField] private List<AudioClip> hurtNoises;
    [SerializeField] private float timeBetweenSingingNoises = 10f;
    private float timeSinceLastSinging;
    
    [SerializeField] private float timeBetweenTalkNoises = 3f;
    private float timeSinceTalkSinging;
    
    public bool isCowering;
    
    
    void Start()
    {
        //other components here
        isCowering = false;
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();
        camera = Camera.main.transform;
        timeSinceLastSinging = Time.time;
        timeSinceTalkSinging = Time.time;
    }
    
    void Update()
    {
        CheckGrounded();
        
        if (Time.time - timeSinceLastSinging > timeBetweenSingingNoises)
        {
            audioSource.clip = singingNoises[UnityEngine.Random.Range(0, singingNoises.Count)];
            audioSource.Play();
        }
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
            rb.velocity = new Vector3(targetDir.x * airSpeed * Time.fixedDeltaTime, rb.velocity.y, targetDir.z * airSpeed * Time.fixedDeltaTime);
        }
        else if (targetDir.Equals(Vector2.zero))
        {
            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
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

    public void TriggerCower()
    {
        isCowering = true;
    }

    /* Events */
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        isMoving = moveInput != Vector2.zero;
        footStepsSource.enabled = isMoving;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            Jump();
        }
    }
    
    public void OnSmash(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            animator.SetTrigger("Smash");
            context.action.Reset();
            
            if (Time.time - timeSinceTalkSinging > timeBetweenTalkNoises)
            {
                audioSource.clip = smashNoises[UnityEngine.Random.Range(0, smashNoises.Count)];
                audioSource.Play();
            }
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

    public void PlayScoreAudios()
    {
        trashPickUpAudioSource.clip = trashPickupNoises[UnityEngine.Random.Range(0, trashPickupNoises.Count)];
        trashPickUpAudioSource.Play();
    }

    public void HurtAudio()
    {
        audioSource.clip = hurtNoises[UnityEngine.Random.Range(0, hurtNoises.Count)];
        audioSource.Play();
    }
}
