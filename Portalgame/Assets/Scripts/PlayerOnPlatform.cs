using UnityEngine;

public class PlayerOnPlatform : MonoBehaviour
{
    [SerializeField] private MoveBackAndForth PlatformScript;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(PlatformScript.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}
