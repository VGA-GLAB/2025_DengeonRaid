using DG.Tweening;
using UnityEngine;

/// <summary>
///         敵を殺せる場合の演出クラス
/// </summary>
public class EnemyDeathPreviewEffect : MonoBehaviour
{
    // EnemyとBossが死んだときで変えられるようにシリアライズにしています
    [Header("死んだときのマーク"), SerializeField]
    private GameObject _deathMark;

    [Header("演出設定")]
    [SerializeField] private float _effectScale;
    [SerializeField] private float _effectDuration;
    [SerializeField] private float _backEffectDuration;

    private Tween _effectTween;

    /// <summary>
    ///         敵を殺せる場合は画像と演出を表示する
    /// </summary>
    public void EnemyCanBeKilledEffect()
    {
        // すでに表示されている場合は何もしない
        // activeSelfはアクティブ状態を返します
        if (_deathMark == null) return;
        if (_deathMark.activeSelf) return;

        // 初期化
        _deathMark.SetActive(true);
        _deathMark.transform.localScale = Vector3.zero;
        _effectTween?.Kill();

        // 演出再生
        _effectTween = _deathMark.transform.DOScale(Vector3.one * _effectScale, _effectDuration)
            .From(Vector3.zero)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                _deathMark.transform.DOScale(1f, _backEffectDuration);
            });
    }

    /// <summary>
    ///         画像と演出を非表示にする
    /// </summary>
    public void ResetEffect()
    {
        if (_deathMark == null) return;
        if (!_deathMark.activeSelf) return;

        _effectTween?.Kill();
        _deathMark.SetActive(false);
    }

    private void Awake()
    {
        if (_deathMark != null)
            _deathMark.SetActive(false);
    }
}
