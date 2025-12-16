using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButtonEvent : MonoBehaviour
{
    [SerializeField] private Image _fadeInImage;
    [SerializeField] private Button _onPreseButton;

    public void OnStartButtonClicked()
    {
     if(_onPreseButton!=null)  _onPreseButton.gameObject.SetActive(false);
        _fadeInImage.gameObject.SetActive(true);
        _fadeInImage.DOFade(1f, 1f).OnComplete(LoadInGameScene);
    }

    private void LoadInGameScene()
    {
        SceneManager.LoadScene("InGameScene");
    }
}