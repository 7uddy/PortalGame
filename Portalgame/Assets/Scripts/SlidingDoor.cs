using System;
using System.Collections;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class SlidingDoor : MonoBehaviour
{
    private bool isOpen = false;
    public bool IsOpen
    {
        get { return isOpen; }
    }
    private bool isPlayerNearby = false;
    public bool IsPlayerNearby
    {
        get { return isPlayerNearby; }
        set { isPlayerNearby = value; }
    }
    private bool isOpening = false;
    public bool IsOpening
    {
        get { return isOpening; }
    }
    private bool isClosing = false;
    public bool IsClosing
    {
        get { return isClosing; }
    }
    [SerializeField] private float Speed = 1.0f;
    [SerializeField] private Vector3 SlideDirection = Vector3.up;
    [SerializeField] private float SlideAmount = 1.9f;
    private Vector3 StartPosition;

    void Start()
    {
        StartPosition = transform.position;
    }
    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if (isOpening || isClosing)
                return;

            if (isOpen == true)
            {
                StartCoroutine(CloseDoor());
            }
            else
            {
                StartCoroutine(OpenDoor());
            }
        }


    }

    public IEnumerator OpenDoor()
    {
        StartPosition = transform.position;
        Vector3 endPosition = StartPosition + SlideDirection * SlideAmount;

        float time = 0;
        isOpening = true;
        isClosing = false;
        while (time < 1)
        {
            transform.position = Vector3.Lerp(StartPosition, endPosition, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }
        transform.position = endPosition;
        isOpening = false;
        isOpen = true;
    }

    public IEnumerator CloseDoor()
    {
        Vector3 endPosition = StartPosition;
        Vector3 start = transform.position;

        float time = 0;
        isClosing = true;
        isOpening = false;
        while (time < 1)
        {
            transform.position = Vector3.Lerp(start, endPosition, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }
        transform.position = endPosition;
        isClosing = false;
        isOpen = false;

    }

}
