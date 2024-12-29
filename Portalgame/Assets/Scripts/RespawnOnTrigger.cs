using UnityEngine;

public class RespawnOnTrigger : MonoBehaviour
{
    [SerializeField] private Vector3 respawnPoint;
    [SerializeField] private GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.transform.position = respawnPoint;
        }
    }
}
