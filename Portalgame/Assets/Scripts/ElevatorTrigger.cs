using UnityEngine;

public class ElevatorTrigger : MonoBehaviour
{
    [SerializeField] private Elevator elevatorScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            elevatorScript.IsPlayerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            elevatorScript.IsPlayerNearby = false;
        }
    }
}
