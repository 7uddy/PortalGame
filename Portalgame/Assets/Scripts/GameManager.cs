using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// Controls loading scene implementation and scene management.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject m_LoadingScreen;
    public Slider Slider;
    public TextMeshProUGUI LoadingText;

    public TextMeshProUGUI TipText;
    public CanvasGroup TipAlphaCanvas;
    private int _currentTipIndex;
    public string[] Tips;

    public Image BackgroundImage;
    public CanvasGroup BackgroundImageAlphaCanvas;
    private int _currentBackgroundIndex;
    public Sprite[] BackgroundImages;

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
    /// Generates the first tip.
    /// </summary>
    private void OnEnable()
    {
        _currentTipIndex = Random.Range(0, Tips.Length);
        TipText.text = Tips[_currentTipIndex];

        _currentBackgroundIndex = Random.Range(0, BackgroundImages.Length);
        BackgroundImage.overrideSprite = BackgroundImages[_currentBackgroundIndex];
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
        StartCoroutine(UpdateBackgroundImage());
        StartCoroutine(UpdateLoadingText());
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
        yield return new WaitForSeconds(10f);
        m_LoadingScreen.SetActive(false);
    }

    /// <summary>
    /// Updates the Loading Text.
    /// </summary>
    /// <returns></returns>
    private IEnumerator UpdateLoadingText()
    {
        int dotCount = 0;
        string baseText = "Loading";

        while (m_LoadingScreen.activeInHierarchy)
        {
            LoadingText.text = baseText + new string('.', dotCount);

            dotCount = (dotCount + 1) % 4;

            yield return new WaitForSeconds(0.3f);
        }
    }

    /// <summary>
    /// Manipulates the switch of background images.
    /// </summary>
    private IEnumerator UpdateBackgroundImage()
    {
        while (m_LoadingScreen.activeInHierarchy)
        {
            yield return new WaitForSeconds(5f);

            _currentBackgroundIndex++;
            if (_currentBackgroundIndex >= BackgroundImages.Length)
            {
                _currentBackgroundIndex = 0;
            }

            BackgroundImage.overrideSprite = BackgroundImages[_currentBackgroundIndex];
        }
    }


    private bool _isChangingTip = false;
    /// <summary>
    /// Updates tip based on user input.
    /// </summary>
    private void Update()
    {
        if (_isChangingTip) return;

        if (Input.GetKeyUp(KeyCode.D))
        {
            ChangeTip(1);
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            ChangeTip(-1);
        }
    }

    /// <summary>
    /// Updates the current tip with the adiacent one on the left/right based on the direction.
    /// </summary>
    /// <param name="direction">Is 1 if the move is in the right direction, -1 in the opposite.</param>
    private void ChangeTip(int direction)
    {
        _isChangingTip = true;
        LeanTween.alphaCanvas(TipAlphaCanvas, 0, 0.5f).setOnComplete(() =>
        {
            _currentTipIndex += direction;

            if (_currentTipIndex >= Tips.Length)
            {
                _currentTipIndex = 0;
            }
            else if (_currentTipIndex < 0)
            {
                _currentTipIndex = Tips.Length - 1;
            }

            TipText.text = Tips[_currentTipIndex];
            LeanTween.alphaCanvas(TipAlphaCanvas, 1, 0.5f).setOnComplete(() =>
            {
                _isChangingTip = false;
            });
        });
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
