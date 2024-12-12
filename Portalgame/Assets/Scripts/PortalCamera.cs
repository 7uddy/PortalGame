using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    [SerializeField] public Transform playerCamera;
    [SerializeField] public Transform portal;
    [SerializeField] public Transform otherPortal;

    // Update is called once per frame
    void Update()
    {
        Vector3 playerOffsetFromPortal = playerCamera.position - otherPortal.position;
        transform.position = portal.position + playerOffsetFromPortal;
        //transform.eulerAngles.y += otherPortal.eulerAngles.y;
    }
}
