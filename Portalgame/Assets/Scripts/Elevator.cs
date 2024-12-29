using System;
using System.Collections;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private SlidingDoor doorScript;
    [SerializeField] private float SmoothTime = 0.3f;
    [SerializeField] private Vector3 Floor2Position;
    private Vector3 Floor1Position;
    private Vector3 velocity = Vector3.zero;

    private bool isAtFloor1 = true;
    private bool isMoving = false;
    private bool isPlayerNearby = false;
    public bool IsPlayerNearby
    {
        get { return isPlayerNearby; }
        set { isPlayerNearby = value; }
    }
    public bool IsAtFloor1 => isAtFloor1;
    public bool IsAtFloor2 => !isAtFloor1;
    public bool IsMoving => isMoving;

    void Start()
    {
        Floor1Position = transform.position;
        Floor2Position += Floor1Position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(HandleInteraction());
        }
    }

    private IEnumerator HandleInteraction()
    {
        if (IsPlayerNearby)
        {
            if (doorScript.IsOpen && !doorScript.IsMoving)
            {
                yield return StartCoroutine(doorScript.CloseDoor());
            }
            if (isMoving)
                yield break;

            if (isAtFloor1)
            {
                yield return StartCoroutine(MoveToFloor(Floor2Position));
            }
            else
            {
                yield return StartCoroutine(MoveToFloor(Floor1Position));
            }
        }
    }

    private IEnumerator MoveToFloor(Vector3 targetPosition)
    {
        isMoving = true;

        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, SmoothTime);
            yield return null;
        }

        transform.position = targetPosition;
        isAtFloor1 = targetPosition == Floor1Position;
        isMoving = false;
        StartCoroutine(doorScript.OpenDoor());
    }
}
