using UnityEngine;
using DG.Tweening;

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
    /// <param name="targetPosition"></param>
    public void PlayDrop(Vector2Int targetPosition, float delay)
    {
        Vector3 targetLocalPos = new Vector3(targetPosition.x, -targetPosition.y, 0);
        transform.localPosition = targetLocalPos + Vector3.up * _dropHeight;

        _s = DOTween.Sequence()
            .AppendCallback(() => transform.localScale = Vector3.one * _expantion)
            .AppendInterval(delay /*  */)
            .Append(transform.DOLocalMoveY(targetLocalPos.y, _dropDuration))
            .SetEase(Ease.OutBack)
            .OnComplete(() => transform.DOScale(1.0f, 0.1f))
            .SetLink(gameObject)
            .Play();
    }
}