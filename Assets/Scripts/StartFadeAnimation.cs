using DG.Tweening;
using TMPro;
using UnityEngine;

public class StartFadeAnimation : MonoBehaviour
{
    [SerializeField] private TMP_Text  _pressStartText;

    void Start()
    {
        _pressStartText.DOFade(0, 0.7f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear);
    }
}
