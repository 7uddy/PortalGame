using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public void OnStartButtonClicked()
    {
        Debug.Log("BUTTON CLICKED: START");
        GameManager.Instance.SwitchToScene(SceneIndexes.GAME);
    }

    public void OnSettingsButtonClicked()
    {
        Debug.Log("BUTTON CLICKED: SETTINGS");
    }

    public void OnExitButtonClicked()
    {
        Debug.Log("BUTTON CLICKED: EXIT");
        GameManager.Instance.ExitApplication();
    }
}
