using NUnit.Framework;
using UnityEngine;

public class SlidingDoorTrigger : MonoBehaviour
{
    public SlidingDoor doorScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorScript.IsPlayerNearby = true;
            if(!doorScript.IsOpen && !doorScript.IsOpening && !doorScript.IsClosing)
            {
                StartCoroutine(doorScript.OpenDoor());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorScript.IsPlayerNearby = false;
            if (doorScript.IsOpen && !doorScript.IsOpening && !doorScript.IsClosing)
            {
                StartCoroutine(doorScript.CloseDoor());
            }
        }
    }
}
