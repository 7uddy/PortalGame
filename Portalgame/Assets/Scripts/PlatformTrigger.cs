using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    [SerializeField] private RotationOnTrigger PlatformScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlatformScript.IsPlayerOnTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlatformScript.IsPlayerOnTrigger = false;
        }
    }

}
