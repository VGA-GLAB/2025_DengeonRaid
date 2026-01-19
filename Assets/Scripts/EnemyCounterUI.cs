using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCounterUI : MonoBehaviour
{
    [SerializeField] private Image _enemyCountGauge;
    [SerializeField] private TextMeshProUGUI _enemyCountText;

    private GameDirector _gameDirector;

    private void Awake()
    {
        _gameDirector = ReferenceManager.Instance.GameDirector;
    }

    public void UpdateEnemyCountUI()
    {
        if (_gameDirector == null) return;

        if (_gameDirector.EnemyCountForBoss <= 0)
        {
            _enemyCountGauge.fillAmount = 0f;
            _enemyCountText.text = "0";
            return;
        }

        int restEnemyCount =
            Mathf.Max(_gameDirector.EnemyCountForBoss - _gameDirector.EnemyCount, 0);

        _enemyCountGauge.fillAmount =
            Mathf.Clamp01((float)restEnemyCount / _gameDirector.EnemyCountForBoss);

        _enemyCountText.text = restEnemyCount.ToString();
    }
}
