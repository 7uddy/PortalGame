using System;
using System.Collections;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class SlidingDoor : MonoBehaviour
{

    public bool IsOpen = false;

    [SerializeField] private float Speed = 1.0f;
    [SerializeField] private Vector3 SlideDirection = Vector3.up;
    [SerializeField] private float SlideAmount = 1.9f;

    private Vector3 StartPosition;
    public bool IsPlayerNearby = false;
    public bool IsOpening = false;
    public bool IsClosing = false;

    void Start()
    {
        StartPosition = transform.position;
    }
    void Update()
    {
        if (IsPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if (IsOpening || IsClosing)
                return;

            if (IsOpen==true)
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
        Vector3 endPosition = StartPosition + SlideDirection * SlideAmount;

        float time = 0;
        IsOpening = true;
        IsClosing = false;
        while (time < 1)
        {
            transform.position = Vector3.Lerp(StartPosition, endPosition, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }
        IsOpening = false;
        IsOpen = true;
    }

    public IEnumerator CloseDoor()
    {
        Vector3 endPosition = StartPosition;
        Vector3 start = transform.position;

        float time = 0;
        IsClosing = true;
        IsOpening = false;
        while (time < 1)
        {
            transform.position = Vector3.Lerp(start, endPosition, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }
        IsClosing = false;
        IsOpen = false;
        
    }
  
}
