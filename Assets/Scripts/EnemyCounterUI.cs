using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCounterUI : MonoBehaviour
{
    [SerializeField]
    private GameDirector _gd;
    [SerializeField]
    private Image _enemyCountGauge;
    [SerializeField]
    private TextMeshProUGUI _enmeyCountText;

    private float _restEnemyCount;

    /// <summary>
    /// 残りの敵の数をUIに反映する処理
    /// </summary>
    public void EnemyCount()
    {
        _restEnemyCount = _gd.EnemyCountForBoss - _gd.EnemyCount;
        if (_restEnemyCount <= 0)
        {
            _restEnemyCount = 0;
        }
        _enemyCountGauge.fillAmount = (_restEnemyCount) / _gd.EnemyCountForBoss;
        _enmeyCountText.text = $"{_restEnemyCount}";
    }
}
