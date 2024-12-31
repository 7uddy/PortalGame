using UnityEngine;

public class ButtonSecondChamber : MonoBehaviour
{
    [SerializeField] private GameObject Laser;
    [SerializeField] private MoveBackAndForth PlatformScript;
    private bool IsNearby = false;

    private void Update()
    {
        if (IsNearby && Input.GetKeyDown(KeyCode.E))
        {
            if(!PlatformScript.IsMoving) Laser.SetActive(!Laser.activeSelf);
            if(PlatformScript.MovingToB)
            {
                StartCoroutine(PlatformScript.MoveToPointA());
            }
            else
            {
                StartCoroutine(PlatformScript.MoveToPointB());
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsNearby = false;
        }
    }
}
