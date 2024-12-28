using UnityEngine;

public class SlidingDoorTrigger : MonoBehaviour
{
    [SerializeField] private SlidingDoor doorScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorScript.IsPlayerNearby = true;
            if (!doorScript.IsOpen && !doorScript.IsMoving)
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
            if (doorScript.IsOpen && !doorScript.IsMoving)
            {
                StartCoroutine(doorScript.CloseDoor());
            }
        }
    }
}
