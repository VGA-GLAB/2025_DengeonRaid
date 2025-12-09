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

    [Header("生成エフェクト")]
    [SerializeField] private GameObject _deleateEffectHpPrefabs;
    [SerializeField] private GameObject _deleateEffectCoinPrefabs;
    [SerializeField] private GameObject _deleateEffectShieldPrefabs;

    // 再生中のエフェクトの数
    private int _effectCnt = 0;
    private ReferenceManager _rm;

    /// <summary>
    /// 消去エフェクト1個分生成時の処理
    /// </summary>
    /// <param name="panel"></param>
    public void OnOneEffectGenerate(Panel panel)
    {
        EffectMove(panel);
        _effectCnt++;
    }

    /// <summary>
    /// 消去エフェクト1個分終了時処理
    /// </summary>
    public void OnOneEffectEnd()
    {
        _effectCnt--;
        if (_effectCnt == 0)
        {
            _rm.GameDirector.PanelResolvingFinished();
        }
    }

    /// <summary>
    ///         UIパネル移動演出
    /// </summary>
    /// <param name="panel"></param>
    private void EffectMove(Panel panel)
    {
        GameObject instPanel;
        if (panel is PotionPanel)
        {
            instPanel = Instantiate(_deleateEffectHpPrefabs, panel.transform.position, Quaternion.identity);
            instPanel.GetComponent<DeletePanelEffect>()?.PanelMove(_targetHpObj, panel, OnOneEffectEnd);
        }
        else if (panel is CoinPanel)
        {
            instPanel = Instantiate(_deleateEffectCoinPrefabs, panel.transform.position, Quaternion.identity);
            instPanel.GetComponent<DeletePanelEffect>()?.PanelMove(_targetCoinObj, panel, OnOneEffectEnd);
        }
        else if (panel is ShieldPanel)
        {
            instPanel = Instantiate(_deleateEffectShieldPrefabs, panel.transform.position, Quaternion.identity);
            instPanel.GetComponent<DeletePanelEffect>()?.PanelMove(_targetShieldObj, panel, OnOneEffectEnd);
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

    private void Start()
    {
        _rm = ReferenceManager.Instance;
        _effectCnt = 0;
    }
}
