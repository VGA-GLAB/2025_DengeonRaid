using UnityEngine;

/// <summary>
///         MissileEffectの管理クラス
/// </summary>
public class MissileEffectManager : MonoBehaviour
{
    public static MissileEffectManager Instance { get; private set; }

    [SerializeField] private GameObject _missileEffectPrefab;

    [Header("参照")]
    [SerializeField] private Transform _spawnPos;
    [SerializeField] private Transform _targetPos;
    [SerializeField] private SpriteRenderer _enemySprite;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    /// <summary>
    ///         敵スプライトを設定する
    /// </summary>
    /// <param name="sprite"></param>
    public void SetEnemySprite(Sprite sprite)
    {
        _enemySprite.sprite = sprite;
        _enemySprite.gameObject.SetActive(true);
    }

    /// <summary>
    ///         ミサイルエフェクトを再生する
    /// </summary>
    public void PlayMissileEffect()
    {
        GameObject missile = Instantiate(_missileEffectPrefab, _spawnPos.position, Quaternion.identity);
        MissileEffect missileEffect = missile.GetComponent<MissileEffect>();
        missileEffect.PlayEffect(_targetPos.position, () =>
        {
            // エフェクトがターゲットに到達したとき
            _enemySprite.gameObject.SetActive(false);
        });
    }
}
