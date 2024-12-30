using UnityEngine;

public class SlidingDoorTrigger : MonoBehaviour
{
    [SerializeField] private SlidingDoor doorScript;
    private bool ToClose=false;
    private void Update()
    {
        if (ToClose && !doorScript.IsPlayerNearby && doorScript.IsOpen && !doorScript.IsMoving)
        {
            StartCoroutine(doorScript.CloseDoor());
            ToClose = false;
        }
    }
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
            else if (doorScript.IsMoving)
            {
                ToClose=true;
            }
        }
    }
}
