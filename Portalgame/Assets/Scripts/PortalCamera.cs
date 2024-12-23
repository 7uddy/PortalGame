using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    [SerializeField] public Transform playerCamera;
    [SerializeField] public Transform portal;
    [SerializeField] public Transform otherPortal;

    // Update is called once per frame
    void Update()
    {
        //Vector3 playerOffsetFromPortal = playerCamera.position - otherPortal.position;
        //transform.position = portal.position + playerOffsetFromPortal;
    
        float angularDifferenceBetweenPortals = Quaternion.Angle(portal.rotation, otherPortal.rotation);

        Quaternion portalRotationDiff = Quaternion.AngleAxis(angularDifferenceBetweenPortals, Vector3.up);
        Vector3 newCameraDirection = portalRotationDiff * playerCamera.forward;
        transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up); 
    }
}
