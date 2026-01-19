using System.Collections.Generic;
using UnityEngine;

/// <summary>
///         MissileEffectの管理クラス
/// </summary>
public class MissileEffectManager : MonoBehaviour
{
    public static MissileEffectManager Instance { get; private set; }

    [Header("参照")]
    [SerializeField] private GameObject _missileEffectPrefab;
    [SerializeField] private Transform _spawnPos;
    [SerializeField] private GameObject _targetObjectPrefab;
    [SerializeField] private ReloadEffect _reloadEffect;

    [Header("並び替え設定")]
    [SerializeField] private Transform _startTargetPos;
    [SerializeField] private Transform _endTargetPos;
    [SerializeField] private float _spacingPosY = 0.5f;

    private List<Sprite> _deadEnemyList = new();

    /// <summary>
    ///         画像情報をリストに追加する
    /// </summary>
    /// <param name="sprite"></param>
    public void SetSprite(Sprite sprite)
    {
        _deadEnemyList.Add(sprite);
    }

    /// <summary>
    ///         リストに入っている敵に対してミサイルエフェクトを再生する
    /// </summary>
    public void PlayMissileEffect()
    {
        // 敵が倒されていなくても呼ばれるのでチェック
        if (_deadEnemyList.Count == 0) return;

        // 配置可能な高さを超えているかどうかを判定
        float totalHeight = (_deadEnemyList.Count - 1) * _spacingPosY;
        float sumHeight = _startTargetPos.position.y - _endTargetPos.position.y;

        bool isOverHeight = totalHeight > sumHeight;

        List<Vector3> positions = new();

        if (!isOverHeight)
        {
            // 高さを超えていない場合は、指定の間隔で配置する
            float posY = 0;
            for (int i = 0; i < _deadEnemyList.Count; i++)
            {
                positions.Add(new Vector3(
                    _startTargetPos.transform.position.x,
                    _startTargetPos.transform.position.y + posY,
                    _startTargetPos.transform.position.z
                    ));

                posY -= _spacingPosY;
            }
        }
        else
        {
            // 高さを超えている場合は、等間隔で配置する
            float interval = sumHeight / (_deadEnemyList.Count - 1);
            float posY = 0;

            for (int i = 0; i < _deadEnemyList.Count; i++)
            {
                positions.Add(new Vector3(
                    _startTargetPos.transform.position.x,
                    _startTargetPos.transform.position.y + posY,
                    _startTargetPos.transform.position.z
                    ));

                posY -= interval;
            }
        }

        // ミサイルエフェクトを生成して再生
        for (int i = 0; i < _deadEnemyList.Count; i++)
        {
            _reloadEffect.Fire();

            Sprite oneSprite = _deadEnemyList[i];
            Vector3 targetPos = positions[i];

            GameObject effectEnemy = Instantiate(_targetObjectPrefab,
                targetPos,
                Quaternion.identity);
            effectEnemy.GetComponent<SpriteRenderer>().sprite = oneSprite;


            GameObject missile = Instantiate(_missileEffectPrefab, _spawnPos.position, Quaternion.identity);
            MissileEffect missileEffect = missile.GetComponent<MissileEffect>();
            missileEffect.PlayEffect(effectEnemy.transform.position, () =>
            {
                // エフェクトがターゲットに到達したとき
                Destroy(effectEnemy);
            });
        }

        _deadEnemyList.Clear();
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}
