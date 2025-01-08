using Unity.VisualScripting;
using UnityEngine;

public class ElevatorTrigger : MonoBehaviour
{
    [SerializeField] private Elevator elevatorScript;
    [SerializeField] private GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            elevatorScript.IsPlayerNearby = true;
            player.transform.SetParent(transform);
            UIHintController.Instance.ShowHint("Press F to use the elevator.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            elevatorScript.IsPlayerNearby = false;
            player.transform.SetParent(null);
            UIHintController.Instance.HideTip();
        }
    }
}
