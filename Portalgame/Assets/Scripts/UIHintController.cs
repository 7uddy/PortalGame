using TMPro;
using UnityEngine;

public class UIHintController : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI TipText;
    [SerializeField]
    public CanvasGroup TipAlphaCanvas;

    private void OnEnable()
    {
        TipAlphaCanvas.alpha = 0;
    }
    public void ShowHint(string hint)
    {
        if (TipAlphaCanvas.alpha != 0)
        {
            LeanTween.alphaCanvas(TipAlphaCanvas, 0, 0.5f).setOnComplete(() =>
            {
                TipText.text = hint;
                LeanTween.alphaCanvas(TipAlphaCanvas, 1, 0.5f);
            });
            return;
        }

        TipText.text = hint;
        LeanTween.alphaCanvas(TipAlphaCanvas, 1, 0.5f);
    }

    public void HideTip()
    {
        LeanTween.alphaCanvas(TipAlphaCanvas, 0, 0.5f);
    }
}
