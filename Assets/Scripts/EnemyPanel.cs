using TMPro;
using UnityEngine;

public class EnemyPanel : Panel
{
    [SerializeField, Header("敵攻撃力")]
    private int _attack;
    [SerializeField, Header("敵シールド")]
    private int _shield;
    [SerializeField, Header("敵シールド耐久度")]
    private int _shieldStrength;
    [SerializeField, Header("敵HP")]
    private int _hp;
    [SerializeField, Header("死亡フラグ")]
    private bool _isDead;

    [Header("敵属性UI")]
    [SerializeField] private TextMeshProUGUI _uiTextAttack;
    [SerializeField] private TextMeshProUGUI _uiTextShield;
    [SerializeField] private TextMeshProUGUI _uiTextHp;
    // 戦闘結果プレビューデータ
    private PreviewEnemyData _preview;

    public int Attack { get => _attack; private set => _attack = value; }
    public int Shield { get => _shield; private set => _shield = value; }
    public int ShieldStrength { get => _shieldStrength; private set => _shieldStrength = value; }
    public int Hp { get => _hp; private set => _hp = value; }
    public bool IsDead { get => _isDead; private set => _isDead = value; }

    public void SetResolvePreview(PreviewEnemyData preview)
    {
        _preview = preview;
    }

    private void Start()
    {
        UpdateAttrDisplay();
    }
    /// <summary>
    /// 戦闘処理で計算した結果プレビューを適用する
    /// </summary>
    public void ApplyPreview()
    {
        if (_preview == null)
        {
            Debug.LogWarning("敵パネルプレビューデータ無し！！");
            return;
        }
        if (_preview.IsDead)
        {
            DestroyThis();
        }
        else
        {
            Hp = _preview.Hp;
            Shield = _preview.Shield;
        }
        _preview = null;
    }

    public override void DestroyThis()
    {
        ReferenceManager.Instance.BoardManager.RemovePanelFromBoard(this);
        Destroy(gameObject);
    }

    /// <summary>
    /// 敵の属性表示を更新する
    /// </summary>
    public void UpdateAttrDisplay()
    {
        _uiTextAttack.text = _attack.ToString();
        _uiTextShield.text = _shield.ToString();
        _uiTextHp.text = _hp.ToString();
    }
}
