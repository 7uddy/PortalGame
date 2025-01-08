using System;
using System.Collections;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class SlidingDoor : MonoBehaviour
{
    [SerializeField] private Elevator elevatorScript;
    private bool isOpen = false;
    public bool IsOpen=> isOpen;
    private bool isPlayerNearby = false;
    public bool IsPlayerNearby
    {
        get { return isPlayerNearby; }
        set { isPlayerNearby = value; }
    }
    private bool isMoving = false;
    public bool IsMoving=> isMoving;
    [SerializeField] private float Speed = 1.0f;
    [SerializeField] private Vector3 SlideDirection = Vector3.up;
    [SerializeField] private float SlideAmount = 1.9f;
    private Vector3 StartPosition;
    private Vector3 EndPosition;

    void Update()
    {
        if (isPlayerNearby && !elevatorScript.IsMoving && Input.GetKeyDown(KeyCode.E))
        {
            if (isMoving)
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
        EndPosition = StartPosition + SlideDirection * SlideAmount;

        yield return StartCoroutine(MoveDoor(StartPosition, EndPosition));
    }

    public IEnumerator CloseDoor()
    {
        EndPosition = StartPosition;
        StartPosition = transform.position;

        yield return StartCoroutine(MoveDoor(StartPosition, EndPosition));
    }

    private IEnumerator MoveDoor(Vector3 from, Vector3 to)
    {
        SoundManager.Instance.PlaySound2D("DoorSoundEffect");
        float time = 0;
        isMoving = true;
        while (time < 1)
        {
            transform.position = Vector3.Lerp(from, to, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }
        transform.position = to;
        isMoving = false;
        isOpen = !isOpen;
    }
}
