using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Loads loading screen images.
/// </summary>
public class SpriteLoader : MonoBehaviour
{
    public static SpriteLoader Instance;

    public List<Sprite> LoadingSceneBackgrounds = new List<Sprite>();

    /// <summary>
    /// Instantiates singleton and loads sprites.
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
        LoadLoadingSceneBackgroundImages();
    }

    /// <summary>
    /// Load the sprites for the loading screen background.
    /// </summary>
    void LoadLoadingSceneBackgroundImages()
    {
        LoadingSceneBackgrounds.Clear();

        Sprite[] sprites = Resources.LoadAll<Sprite>("Images/loadingScreen");

        foreach (Sprite sprite in sprites)
        {
            LoadingSceneBackgrounds.Add(sprite);
        }
        Debug.Log("NUMBER OF LOADING SCENE BACKGROUNDS READ: " + LoadingSceneBackgrounds.Count);
    }
}
