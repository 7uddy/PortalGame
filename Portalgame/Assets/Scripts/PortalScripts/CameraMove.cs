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
        //var rotation = new Vector2(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
        var targetEuler = TargetRotation.eulerAngles + (Vector3)playerCamera.transform.eulerAngles * cameraSpeed;
        if (targetEuler.x > 180.0f)
        {
            targetEuler.x -= 360.0f;
        }
        targetEuler.x = Mathf.Clamp(targetEuler.x, -75.0f, 75.0f);
        
        TargetRotation = Quaternion.Euler(targetEuler);

        //playerCamera.transform.rotation = Quaternion.Slerp(playerCamera.transform.rotation, TargetRotation, Time.deltaTime * 1.0f);
    }

    public void ResetTargetRotation()
    {
        TargetRotation = Quaternion.LookRotation(playerCamera.transform.forward, Vector3.up);
    }
}
