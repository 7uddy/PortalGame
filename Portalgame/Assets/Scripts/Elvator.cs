using System;
using System.Collections;
using UnityEngine;

public class Elvator : MonoBehaviour
{
    [SerializeField] private SlidingDoor doorScript;
    [SerializeField] private float Speed = 1.0f;
    [SerializeField] private float SmoothTime = 0.3f;
    [SerializeField] private Vector3 Floor2Position;
    [SerializeField] private Vector3 MoveDirection = Vector3.up;
    private Vector3 Floor1Position;
    private Vector3 velocity = Vector3.zero;

    private bool isAtFloor1 = true;
    private bool isMoving = false;

    public bool IsAtFloor1 => isAtFloor1;
    public bool IsAtFloor2 => !isAtFloor1;
    public bool IsMoving => isMoving;

    void Start()
    {
        Floor1Position = transform.position;
    }

    void Update()
    {
        if(doorScript.IsPlayerNearby && !doorScript.IsOpen && !doorScript.IsOpening && !doorScript.IsClosing && Input.GetKeyDown(KeyCode.F))
        {
           if (isMoving)
                return;
            if (isAtFloor1)
            {
                StartCoroutine(MoveToFloor(Floor2Position));
            }
            else
            {
                StartCoroutine(MoveToFloor(Floor1Position));
            }
        }
        
    }

    private IEnumerator MoveToFloor(Vector3 targetPosition)
    {
        isMoving = true;
        float time = 0f;

         while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, SmoothTime);
            yield return null;
            time += Time.deltaTime * Speed;
        }

        transform.position = targetPosition;
        isAtFloor1 = targetPosition == Floor1Position;
        isMoving = false;
    }
}
