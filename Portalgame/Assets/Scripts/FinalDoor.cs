using UnityEngine;

public class FinalDoor : MonoBehaviour
{
    private bool IsPlayerNearby = false;

    private void Update()
    {
        if (IsPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsPlayerNearby = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsPlayerNearby = false;
        }
    }
}
