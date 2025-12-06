using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultButtonEvent : MonoBehaviour
{
    [SerializeField] private Image _fadeInImage;

    public void OnButtonClicked()
    {
        _fadeInImage.gameObject.SetActive(true);
        _fadeInImage.DOFade(1f, 1f).OnComplete(LoadTitleScene);
    }

    private void LoadTitleScene()
    {
        SceneManager.LoadScene("Title");
    }
}