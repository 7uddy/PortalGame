using UnityEngine;

public class FinalDoor : MonoBehaviour
{
    private bool IsPlayerNearby = false;

    private void Update()
    {
        if (IsPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            GameManager.Instance.SwitchToScene(SceneIndexes.TITLE_SCREEN);
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
