using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FPSController : PortalableObject
{
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;
    public LayerMask groundLayer;

    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    private Rigidbody rb;
    private Animator anim;
    private bool isGrounded;
    private bool jumpCooldown; // Prevents jump spamming
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    public bool canMove = true;

    private CameraMove cameraMove;

    protected override void Awake()
    {
        base.Awake();
        cameraMove = GetComponent<CameraMove>();
        anim = gameObject.GetComponent<Animator>();

        base.IsPlayer = true;
    }

    public override void Warp()
    {
        base.Warp();

        Quaternion relativeRot = Quaternion.Inverse(inPortal.transform.rotation) * playerCamera.transform.rotation;
        relativeRot = halfTurn * relativeRot;
        playerCamera.transform.rotation = outPortal.transform.rotation * relativeRot;

        cameraMove.ResetTargetRotation();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevent physics-based rotation
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        cameraMove = GetComponent<CameraMove>();
        cameraMove.moveSpeed = walkSpeed;
        cameraMove.cameraSpeed = lookSpeed;
    }

    void Update()
    {
        HandleRotation();
        CheckKey();
    }
    
    void FixedUpdate()
    {
        HandleMovement();
    }

    private bool IsMoving = false;
    void HandleMovement()
    {
        // Check if grounded using a raycast
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f, groundLayer);

        // Get input directions
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        // Determine speed
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float speed = isRunning ? runSpeed : walkSpeed;

        // Calculate movement direction
        Vector3 forward = transform.forward * inputZ;
        Vector3 right = transform.right * inputX;
        moveDirection = (forward + right).normalized * speed;

        if (moveDirection.magnitude > 0 && !IsMoving && !gameObject.transform.parent)
        {
            SoundManager.Instance.PlaySound2D("PlayerMovementSoundEffect");
            IsMoving = true;
        }
        else if(moveDirection.magnitude == 0 && IsMoving)
        {
            SoundManager.Instance.StopSound();
            IsMoving = false;
        }

        // Preserve Y velocity (gravity or jumping)
        float yVelocity = rb.linearVelocity.y;

        if (!isGrounded)
        {
            // Apply gravity when not grounded
            yVelocity -= gravity * Time.fixedDeltaTime;
        }

        // Jump logic
        if (Input.GetButtonDown("Jump") && canMove && isGrounded && !jumpCooldown)
        {
            yVelocity = jumpPower;
            StartCoroutine(ResetJumpCooldown());
        }

        // Apply movement with preserved Y velocity
        rb.linearVelocity = new Vector3(moveDirection.x, yVelocity, moveDirection.z);
    }

    IEnumerator ResetJumpCooldown()
    {
        jumpCooldown = true;
        yield return new WaitForSeconds(0.2f); // Prevents jump spamming
        jumpCooldown = false;
    }

    void HandleRotation()
    {
        if (canMove && !PauseMenuController.GameIsPaused)
        {
            // Handle camera rotation (Mouse Y)
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

            // Handle character rotation (Mouse X)
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }

    void CheckKey()
    {
        // Walk forward
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (!anim.GetBool("Walk_Anim"))
                anim.SetBool("Walk_Anim", true);
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetBool("Walk_Anim", false);
        }

        // Walk backward
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (!anim.GetBool("Walk_Backward_Anim"))
                anim.SetBool("Walk_Backward_Anim", true);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            anim.SetBool("Walk_Backward_Anim", false);
        }
    }
}
