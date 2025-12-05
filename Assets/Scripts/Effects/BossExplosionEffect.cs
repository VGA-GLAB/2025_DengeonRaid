using System.Collections;
using UnityEngine;

/// <summary>
///     ボスパネル上のの爆発エフェクトを制御するクラス
/// </summary>
public class BossExplosionEffectController : MonoBehaviour
{
    [Header("爆発エフェクト参照")]
    [SerializeField] private GameObject[] _explosions;
    [Header("爆発の表示間隔")]
    [SerializeField] private float _explosionInterval = 0.3f;

    public void PlayExplosions()
    {
        StartCoroutine(PlayExplosionsCoroutine());
    }

    private IEnumerator PlayExplosionsCoroutine()
    {
        foreach (var explosion in _explosions)
        {
            explosion.SetActive(true);
            yield return new WaitForSeconds(_explosionInterval);
        }
    }
}
