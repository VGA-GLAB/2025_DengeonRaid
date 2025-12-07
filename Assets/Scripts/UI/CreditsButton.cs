using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CreditsButton : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Button _creditButton;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _fadeDuration = 1.0f;

    private void Start()
    {
        _canvasGroup.alpha = 0f;
        _canvas.gameObject.SetActive(false);
    }

    public void OnClickCredit()
    {
       
        _canvas.gameObject.SetActive(true);

        _canvasGroup.alpha = 0f;

        _canvasGroup.DOFade(1f, _fadeDuration);

        _creditButton.gameObject.SetActive(false);
    }

    public void OnclickBack()
    {
        _canvasGroup.DOFade(0f, _fadeDuration)
            .OnComplete(() =>
            {
                _canvas.gameObject.SetActive(false);
                _creditButton.gameObject.SetActive(true);
            });
    }
}