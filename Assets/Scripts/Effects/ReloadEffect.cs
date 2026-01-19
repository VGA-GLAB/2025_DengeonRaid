using DG.Tweening;
using UnityEngine;

public class ReloadEffect : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _muzzle;

    private Tween _tween;

    public void Fire()
    {
        _tween?.Kill();
        _bulletPrefab.SetActive(false);
    }
}
