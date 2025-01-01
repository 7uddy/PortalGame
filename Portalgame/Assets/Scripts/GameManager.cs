using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls loading scene implementation and scene management.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject m_LoadingScreen;
    public Slider Slider;

    /// <summary>
    /// Instantiates singleton and disables loading scene.
    /// </summary>
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        m_LoadingScreen.SetActive(false);
    }

    /// <summary>
    /// Switches the scene to the one received as parameter. Starts coroutine related to loading screen.
    /// </summary>
    /// <param name="sceneIndex">Desired scene's index.</param>
    public void SwitchToScene(SceneIndexes sceneIndex)
    {
        Debug.Log("LOADING SCENE: " +  sceneIndex);
        m_LoadingScreen.SetActive(true);
        Slider.value = 0;
        StartCoroutine(SwitchToSceneAsync(sceneIndex.ConvertEnumToInt()));
    }

    /// <summary>
    /// Loads the received index's scene asyncroniously.
    /// </summary>
    /// <param name="id">Desired scene's index.</param>
    private IEnumerator SwitchToSceneAsync(int id)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(id);
        while (!asyncLoad.isDone)
        {
            Slider.value = asyncLoad.progress;
            yield return null;
        }
        yield return new WaitForSeconds(0.2f);
        m_LoadingScreen.SetActive(false);
    }
    
    /// <summary>
    /// Closes Application/Editor based on the environment it is launched in.
    /// </summary>
    public void ExitApplication()
    {
#if !UNITY_EDITOR
        Application.Quit();
#else
        EditorApplication.isPlaying = false;
#endif
    }
}
