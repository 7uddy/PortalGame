using UnityEngine;

public class PortalScript : MonoBehaviour
{
    [SerializeField] public Transform targetPortal;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetPositionAndRotation(targetPortal.position, targetPortal.rotation);
        }
    }
}
