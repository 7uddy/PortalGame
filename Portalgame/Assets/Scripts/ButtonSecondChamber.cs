using UnityEngine;

public class ButtonSecondChamber : MonoBehaviour
{
    [SerializeField] private GameObject Laser;
    [SerializeField] private SlidingDoor Platform;
    private bool IsNearby = false;

    private void Update()
    {
        if (IsNearby && Input.GetKeyDown(KeyCode.E))
        {
            Laser.SetActive(false);
            StartCoroutine(Platform.OpenDoor());
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
