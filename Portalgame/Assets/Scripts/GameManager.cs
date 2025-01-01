using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

//Controls the transition between scenes.
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SwitchToScene(SceneIndexes sceneIndex)
    {
        Debug.Log("LOADING SCENE " +  sceneIndex);
        SceneManager.LoadScene(SceneHelper.ConvertEnumToInt(sceneIndex));
    }

    public void ExitApplication()
    {
#if !UNITY_EDITOR
        Application.Quit();
#else
        EditorApplication.isPlaying = false;
#endif
    }
}
