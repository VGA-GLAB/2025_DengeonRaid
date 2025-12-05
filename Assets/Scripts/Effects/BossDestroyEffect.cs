using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class BossDestroyEffect : MonoBehaviour
{
    SpriteRenderer _renderer;

    [SerializeField, Header("一回目のフェードアウト速度")]
    private float _firstFadeOutTime;

    [SerializeField, Header("一回目の点滅と二回目の点滅の間の時間")]
    private float _firstInterval;

    [SerializeField, Header("一回目のフェードイン速度")]
    private float _firstFadeInTime;

    [SerializeField, Header("二回目の点滅の持続時間")]
    private float _secondDuration;

    [SerializeField, Header("二回目のフェードアウト速度")]
    private float _secondFadeOutTime;

    [SerializeField, Header("二回目の点滅と三回目の点滅の間の時間")]
    private float _secondInterval;

    [SerializeField, Header("二回目のフェードイン速度")]
    private float _secondFadeInTime;

    [SerializeField, Header("三回目の点滅の持続時間")]
    private float _thirdDuration;

    [SerializeField, Header("三回目のフェードアウト速度")]
    private float _thirdFadeOutTime;

    private BossPanel _boss;


    public void BossDeathEffect(Action callback)
    {
        _boss = ReferenceManager.Instance.BossPanel;
        _boss.gameObject.GetComponent<BossExplosionEffectController>().PlayExplosions();
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.color = new Color(1, 1, 1, 1);
        DOTween.Sequence()
            .Append(_renderer.material.DOFade(0, _firstFadeOutTime))
            .AppendInterval(_firstInterval)
            .Append(_renderer.material.DOFade(1, _firstFadeInTime))
            .AppendInterval(_secondDuration)
            .Append(_renderer.material.DOFade(0, _secondFadeOutTime))
            .AppendInterval(_secondInterval)
            .Append(_renderer.material.DOFade(1, _secondFadeInTime))
            .AppendInterval(_thirdDuration)
            .AppendCallback(() => DestroyBoss())
            .Append(_renderer.material.DOFade(0, _thirdFadeOutTime))
            .OnComplete(callback.Invoke);
    }

    private void DestroyBoss()
    {
        Destroy(_boss.gameObject);
    }
}
