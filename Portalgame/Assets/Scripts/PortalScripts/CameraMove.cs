using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private Camera playerCamera;
    
    public float moveSpeed = 3.0f;
    public float cameraSpeed = 2.5f;

    public Quaternion TargetRotation { private set; get; }
    
    private Vector3 moveVector = Vector3.zero;
   

    private new Rigidbody rigidbody;

    private void Awake()
    { 
        rigidbody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;

        TargetRotation = transform.rotation;
    }

    private void Update()
    {
        TargetRotation = Quaternion.Euler(playerCamera.transform.eulerAngles);

        //playerCamera.transform.rotation = Quaternion.Slerp(transform.rotation, TargetRotation, Time.deltaTime * 15.0f);
    }

    //private void FixedUpdate()
    //{
    //    Vector3 newVelocity = playerCamera.transform.TransformDirection(moveVector);
    //    newVelocity.y += playerCamera.transform.position.y * moveSpeed;
    //    //rigidbody.linearVelocity = newVelocity;

    //    //Vector3 newVelocity = transform.TransformDirection(moveVector);
    //    //newVelocity.y += transform.position.y * moveSpeed;
    //    //rigidbody.linearVelocity = newVelocity;
    //}

    public void ResetTargetRotation()
    {
        TargetRotation = Quaternion.LookRotation(playerCamera.transform.forward, Vector3.up);
    }
}
