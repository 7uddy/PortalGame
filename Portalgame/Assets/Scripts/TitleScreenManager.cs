using UnityEngine;

public class TitleScreenManager : MonoBehaviour
{
    public GameObject m_MainMenuObject;
    public GameObject m_SettingsMenuObject;

    private void Awake()
    {
        m_MainMenuObject.SetActive(true);
        m_SettingsMenuObject.SetActive(false);
    }
    public void OnStartButtonClicked()
    {
        Debug.Log("BUTTON CLICKED: START");
        GameManager.Instance.SwitchToScene(SceneIndexes.GAME);
    }

    public void OnSettingsButtonClicked()
    {
        Debug.Log("BUTTON CLICKED: SETTINGS");
        m_SettingsMenuObject.SetActive(true);
        m_MainMenuObject.SetActive(false);
    }

    public void OnExitButtonClicked()
    {
        Debug.Log("BUTTON CLICKED: EXIT");
        GameManager.Instance.ExitApplication();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            m_SettingsMenuObject.SetActive(!m_SettingsMenuObject.activeSelf);
            m_MainMenuObject.SetActive(!m_MainMenuObject.activeSelf);
        }
    }
}
