using NUnit.Framework.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSController : PortalableObject
{
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;

    //Vector3 rot = Vector3.zero;
    //float rotSpeed = 40f;
    Animator anim;

    public float lookSpeed = 2f;
    public float lookXLimit = 45f;


    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;

    private CameraMove cameraMove;
    protected override void Awake()
    {
        base.Awake();
        cameraMove = GetComponent<CameraMove>();
        anim = gameObject.GetComponent<Animator>();
        //gameObject.transform.eulerAngles = rot;
    }

    public override void Warp()
    {
        base.Warp();
        cameraMove.ResetTargetRotation();
    }

    CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        cameraMove = GetComponent<CameraMove>();
        cameraMove.moveSpeed = walkSpeed;
        cameraMove.cameraSpeed = lookSpeed;
    }

    void Update()
    {

        #region Handles Movment
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        CheckKey();

        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);


        #endregion

        #region Handles Jumping
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        #endregion

        #region Handles Rotation
        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        #endregion
    }

    void CheckKey()
    {
        // Walk forward
        if (Input.GetKeyDown(KeyCode.W))
        {
            if(!anim.GetBool("Walk_Anim"))
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