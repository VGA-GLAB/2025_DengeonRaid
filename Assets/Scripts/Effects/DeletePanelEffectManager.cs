using UnityEngine;

/// <summary>
///         パネル演出の管理クラス
/// </summary>
public class DeletePanelEffectManager : MonoBehaviour
{
    public static DeletePanelEffectManager Instance { get; private set; }

    [Header("移動場所")]
    [SerializeField] private Transform _targetHpObj;
    [SerializeField] private Transform _targetCoinObj;
    [SerializeField] private Transform _targetShieldObj;

    [SerializeField, Header("経由地点")]
    private Transform _viaObj;

    [Header("生成エフェクト")]
    [SerializeField] private GameObject _deleateEffectHpPrefabs;
    [SerializeField] private GameObject _deleateEffectCoinPrefabs;
    [SerializeField] private GameObject _deleateEffectShieldPrefabs;

    /// <summary>
    ///         UIパネル移動演出
    /// </summary>
    /// <param name="panel"></param>
    public void EffectMove(Panel panel)
    {
        if (panel is PotionPanel)
        {
            GameObject instPanel = Instantiate(_deleateEffectHpPrefabs);
            instPanel.GetComponent<DeletePanelEffect>()?.PanelMove(_viaObj, _targetHpObj);
         }
        else if (panel is CoinPanel)
        {
            GameObject instPanel = Instantiate(_deleateEffectCoinPrefabs);
            instPanel.GetComponent<DeletePanelEffect>()?.PanelMove(_viaObj, _targetCoinObj);
        }
        else if (panel is ShieldPanel)
        {
            GameObject instPanel = Instantiate(_deleateEffectShieldPrefabs);
            instPanel.GetComponent<DeletePanelEffect>()?.PanelMove(_viaObj, _targetShieldObj);
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
