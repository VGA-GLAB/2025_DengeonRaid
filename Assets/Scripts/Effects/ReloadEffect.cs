using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///         リロードの演出をするクラス
/// </summary>
public class ReloadEffect : MonoBehaviour
{
    [Header("参照")]
    [SerializeField, Tooltip("リロードする弾のプレハブ")] private GameObject _bulletPrefab;
    [SerializeField, Tooltip("銃口の弾")] private GameObject _muzzleBullet;
    [SerializeField] private Transform _muzzle;

    [Header("リロードの角度設定等")]
    [SerializeField, Tooltip("弧を描く中心")] private Transform _magazineCenter;
    [SerializeField] private float _startAngle;
    [SerializeField] private float _endAngle;
    [SerializeField] private float _radius;

    [Header("リロードの動き設定")]
    [SerializeField] private Transform _reloadSpawnPos;
    [SerializeField] private Transform _reloadViaPos;
    [SerializeField] private float _reloadMoveDuration;
    [SerializeField] private float _shiftDuration;
    [SerializeField] private Ease _shiftEase;

    [Header("弾の数")]
    [SerializeField] private int _bulletCapacity;

    private readonly List<GameObject> _magazineBullets = new();
    private bool _isReloading;

    /// <summary>
    /// 銃口の弾を消す
    /// </summary>
    public void FireVisual()
    {
        if (_muzzleBullet != null)
            _muzzleBullet.SetActive(false);
    }

    /// <summary>
    /// 弾をリロードする
    /// 既存の弾を銃口側に寄せる
    /// </summary>
    public Sequence ReloadOne()
    {
        if (_isReloading) return null;

        _isReloading = true;

        Sequence seq = DOTween.Sequence();

        // 既存マガジン弾を銃口側へ詰める
        for (int i = 0; i < _magazineBullets.Count; i++)
        {
            GameObject b = _magazineBullets[i];

            // 基本位置に一旦揃える
            Vector3 to = GetMagazineSlotPos(i);
            b.transform.DOKill();
            seq.Join(b.transform.DOMove(to, _shiftDuration).SetEase(_shiftEase));
        }

        // 銃口に一番近い弾が銃口に乗る演出にするため、index0を飛ばす弾として使う
        GameObject carryBullet = _magazineBullets[0];

        // carryBullet が銃口へ行く前に、一旦見えるようにして spawn位置へ
        seq.AppendCallback(() =>
        {
            carryBullet.SetActive(true);
            carryBullet.transform.DOKill();
            carryBullet.transform.position = _reloadSpawnPos.position;
        });

        // 生成地点から経由地点、最終地点へ
        seq.Append(
            carryBullet.transform.DOPath(
                    new Vector3[] { _reloadViaPos.position, _muzzle.position },
                    _reloadMoveDuration,
                    PathType.CatmullRom
                )
                .SetEase(Ease.OutSine)
        );

        // 銃口に乗ったら消し、銃口の弾の見た目をONにする
        seq.AppendCallback(() =>
        {
            CRIAudioManager.CRISEManager.Play("SE_RocketReload");
            if (carryBullet != null)
                carryBullet.SetActive(false);

            if (_muzzleBullet != null)
                _muzzleBullet.SetActive(true);
        });

        // マガジン表示を整列し直す
        seq.AppendCallback(() =>
        {
            ArrangeMagazineInstant();
        });

        seq.OnComplete(() =>
        {
            _isReloading = false;
        });

        return seq;
    }

    /// <summary>
    /// 弾を指定した分生成する
    /// </summary>
    private void InitBulletSlots()
    {
        for (int i = 0; i < _bulletCapacity; i++)
        {
            GameObject bullet = Instantiate(_bulletPrefab, transform);
            _magazineBullets.Add(bullet);
        }
    }

    /// <summary>
    /// マガジンの弾を一瞬で整列する
    /// </summary>
    private void ArrangeMagazineInstant()
    {
        for (int i = 0; i < _magazineBullets.Count; i++)
        {
            _magazineBullets[i].SetActive(true);
            _magazineBullets[i].transform.position = GetMagazineSlotPos(i);
        }
    }

    /// <summary>
    /// マガジン内のslot位置を取得する
    /// </summary>
    private Vector3 GetMagazineSlotPos(int index)
    {
        // index0が銃口寄りになるように、angleは end→start に向かって並べる
        float denom = (_bulletCapacity - 1);
        float t = (float)index / denom;
        float angleDeg = Mathf.Lerp(_endAngle, _startAngle, t);
        float angleRad = angleDeg * Mathf.Deg2Rad;

        Vector3 offset = new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad), 0f) * _radius;
        return _magazineCenter.position + offset;
    }

    private void Awake()
    {
        InitBulletSlots();
        ArrangeMagazineInstant();

        if (_muzzleBullet != null)
            _muzzleBullet.SetActive(true);
    }
}
