using UnityEngine;
using DG.Tweening;
using System;

/// <summary>
///         パネルの落下演出
/// </summary>
public class PanelDropEffect : MonoBehaviour
{
    [SerializeField] private float _dropHeight = 10f;
    [SerializeField] private float _dropDuration = 0.5f;
    [SerializeField] private float _expantion = 1.1f;

    private Sequence _s;

    /// <summary>
    ///         パネルの落下演出を再生する
    /// </summary>
    /// <param name="targetPos"></param>
    public void PlayDrop(Vector3 fromPos, Vector3 targetPos, float delay, Action callback)
    {
        transform.localPosition = fromPos;

        _s = DOTween.Sequence()
            .AppendCallback(() => transform.localScale = Vector3.one * _expantion)
            .AppendInterval(delay /*  */)
            .Append(transform.DOLocalMove(targetPos, _dropDuration))
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                transform.DOScale(1.0f, 0.1f).OnComplete(() => callback.Invoke());
            })
            .SetLink(gameObject)
            .Play();
    }
}