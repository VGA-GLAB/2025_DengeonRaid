using UnityEngine;
using DG.Tweening;
using System;

/// <summary>
///         敵が死んだ時に呼ばれるエフェクト
/// </summary>
public class MissileEffect : MonoBehaviour
{
    [SerializeField] private float _missileDuration = 10f;
    [SerializeField] private GameObject _explosionEffectPrefab;
    [SerializeField] AnimationCurve _missileEase;
    private Sequence _s;
    private Vector3 _targetPos;

    /// <summary>
    ///         ミサイルを飛ばすエフェクトを再生する
    /// </summary>
    /// <param name="targetPos">着弾地点</param>
    /// <param name="OnHit">敵にミサイル到達時のイベント</param>
    public void PlayEffect(Vector3 targetPos, Action OnHit)
    {
        _targetPos = targetPos;
        // 向きをターゲットに合わせる
        Vector2 direction = (_targetPos - this.transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.Euler(0, 0, angle - 90);

        // ミサイル移動
        _s = DOTween.Sequence()
            .Append(transform.DOMove(_targetPos, _missileDuration))
            .SetEase(_missileEase)
            .OnComplete(() =>
            {
                Instantiate(_explosionEffectPrefab, this.transform.position, Quaternion.identity);
                OnHit?.Invoke();

                Destroy(gameObject);
            });
    }
}
