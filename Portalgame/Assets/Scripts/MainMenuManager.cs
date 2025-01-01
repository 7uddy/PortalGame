using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public void OnStartButtonClicked()
    {
        Debug.Log("START BUTTON CLICKED.");
        GameManager.Instance.SwitchToScene(SceneIndexes.GAME);
    }

    public void OnSettingsButtonClicked()
    {
        Debug.Log("SETTINGS BUTTON CLICKED.");
    }

    public void OnExitButtonClicked()
    {
        Debug.Log("EXIT BUTTON CLICKED.");
        GameManager.Instance.ExitApplication();
    }
}
