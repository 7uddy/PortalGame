using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Loads tips from file.
/// </summary>
public class TipsLoader : MonoBehaviour
{
    public static TipsLoader Instance;

    [NonSerialized]
    public List<Tip> Tips = new List<Tip>();

    /// <summary>
    /// Instantiates singleton and loads tips.
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
        LoadTipsFromFile();
    }

    /// <summary>
    /// Load the tips from the JSON file in Resources.
    /// </summary>
    void LoadTipsFromFile()
    {
        Tips.Clear();

        TextAsset jsonFile = Resources.Load<TextAsset>("Data/tips");

        if (jsonFile != null)
        {
            try
            {
                Tips = JsonUtility.FromJson<TipListWrapper>(jsonFile.ToString()).Tips;
                Debug.Log("LOADED NUMBER OF TIPS: " + Tips.Count);

                ShuffleTips();
            }
            catch (Exception e)
            {
                Debug.LogError("FAILED TO READ TIPS FROM JSON FILE: " + e.Message);
            }
        }
        else
        {
            Debug.LogError("TIPS FILE NOT FOUND.");
        }
    }
    private void ShuffleTips()
    {
        System.Random rng = new System.Random();
        int n = Tips.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            (Tips[n], Tips[k]) = (Tips[k], Tips[n]);
        }
        Debug.Log("TIPS SHUFFLED.");
    }
}

[Serializable]
public struct Tip
{
    public string Message;
}

[Serializable]
public class TipListWrapper
{
    public List<Tip> Tips;
}
