using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButtonEvent : MonoBehaviour
{
    [SerializeField] private Image _fadeInImage;

    public void OnStartButtonClicked()
    {
        _fadeInImage.gameObject.SetActive(true);
        _fadeInImage.DOFade(1f, 1f).OnComplete(LoadInGameScene);
    }

    private void LoadInGameScene()
    {
        SceneManager.LoadScene("InGameScene");
    }
}