using TMPro;
using UnityEngine;

public class EnemyPanel : Panel
{
    [SerializeField, Header("敵攻撃力")]
    protected int _attack;
    [SerializeField, Header("敵シールド")]
    protected int _shield;
    [SerializeField, Header("敵シールド耐久度")]
    protected int _shieldStrength;
    [SerializeField, Header("敵HP")]
    protected int _hp;
    [SerializeField, Header("撃破Exp")]
    protected int _killExp;
    [SerializeField, Header("死亡フラグ")]
    protected bool _isDead;

    [Header("敵属性UI")]
    [SerializeField] protected TextMeshProUGUI _uiTextAttack;
    [SerializeField] protected TextMeshProUGUI _uiTextShield;
    [SerializeField] protected TextMeshProUGUI _uiTextHp;
    // 戦闘結果プレビューデータ
    protected PreviewEnemyData _preview;

    public int Attack { get => _attack; protected set => _attack = value; }
    public int Shield { get => _shield; protected set => _shield = value; }
    public int ShieldStrength { get => _shieldStrength; protected set => _shieldStrength = value; }
    public int Hp { get => _hp; protected set => _hp = value; }
    public int KillExp { get => _killExp; protected set => _killExp = value; }
    public bool IsDead { get => _isDead; protected set => _isDead = value; }

    public void SetResolvePreview(PreviewEnemyData preview)
    {
        _preview = preview;
    }

    protected void Start()
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
        ReferenceManager.Instance.GameDirector.CountEnemyKill(1);
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
