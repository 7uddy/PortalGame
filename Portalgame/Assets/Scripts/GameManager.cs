using UnityEngine;
using UnityEngine.SceneManagement;

//Controls the transition between scenes.
public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    private void Awake()
    {
        _instance = this;
        SceneManager.LoadSceneAsync((int)SceneIndexes.TITLE_SCREEN, LoadSceneMode.Additive);
    }

    public void LoadGame()
    {
        SceneManager.UnloadSceneAsync((int)SceneIndexes.TITLE_SCREEN);
        SceneManager.LoadSceneAsync((int)SceneIndexes.GAME, LoadSceneMode.Additive);
    }
}
