using System;
using System.Collections;
using UnityEngine;
/// <summary>
/// Controls the pause menu in game.
/// </summary>
public class PauseMenuController : MonoBehaviour
{
    [NonSerialized]
    public static bool GameIsPaused;
    [SerializeField]
    public GameObject m_PauseMenuObject;

    [SerializeField] 
    public GameObject m_PauseMainMenuObject;

    [SerializeField]
    public GameObject m_PauseSettingsMenuObject;

    /// <summary>
    /// Makes pause menu object disabled.
    /// </summary>
    void Start()
    {
        GameIsPaused = false;
        m_PauseMenuObject.SetActive(false);
    }

    /// <summary>
    /// Check for user input.
    /// </summary>
    void Update()
    {
        if (GameManager.Instance.IsLoading)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            } 
            else
            {
                Pause();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            GameManager.Instance.SwitchToScene(SceneIndexes.GAME);
        }
    }

    /// <summary>
    /// Resumes game and hides pause menu.
    /// </summary>
    private void Resume()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        m_PauseMenuObject.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    /// <summary>
    /// Pauses game and shows pause menu.
    /// </summary>
    private void Pause()
    {
        m_PauseMenuObject.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnResumeButtonClick()
    {
        Debug.Log("BUTTON CLICKED: RESUME");
        SoundManager.Instance.PlaySound2D("UIClick");
        Resume();
    }
    /// <summary>
    /// 
    /// </summary>
    public void OnSettingsButtonClicked()
    {
        Debug.Log("BUTTON CLICKED: GO TO SETTINGS MENU");
        SoundManager.Instance.PlaySound2D("UIClick");

        m_PauseMainMenuObject.SetActive(!m_PauseMainMenuObject.activeSelf);
        m_PauseSettingsMenuObject.SetActive(!m_PauseSettingsMenuObject.activeSelf);
    }
    /// <summary>
    /// 
    /// </summary>
    public void OnGoToMainMenuButtonClicked()
    {
        Debug.Log("BUTTON CLICKED: GO TO MAIN MENU");
        SoundManager.Instance.PlaySound2D("UIClick");
        Time.timeScale = 1f;
        GameIsPaused = false;
        GameManager.Instance.SwitchToScene(SceneIndexes.TITLE_SCREEN);
    }
    /// <summary>
    /// 
    /// </summary>
    public void OnQuitButtonClicked()
    {
        Debug.Log("BUTTON CLICKED: EXIT APPLICATION");
        SoundManager.Instance.PlaySound2D("UIClick");

        StartCoroutine(ExitAfterSound());
    }

    private IEnumerator ExitAfterSound()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        GameManager.Instance.ExitApplication();
    }
}
