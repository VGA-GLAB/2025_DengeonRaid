using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCounterUI : MonoBehaviour
{
    [SerializeField]
    private Image _enemyCountGauge;
    [SerializeField]
    private TextMeshProUGUI _enmeyCountText;
    public void EnemyCount()
    {
        _enemyCountGauge.fillAmount = 0;
        _enmeyCountText.text = "a";
    }
}
