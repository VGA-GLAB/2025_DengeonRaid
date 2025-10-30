using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int _hp;
    [SerializeField] private int _hpMax;
    [SerializeField] private int _exp;
    [SerializeField] private int _expmax;
    [SerializeField] private int _gold;
    [SerializeField] private int _goldMax;
    [SerializeField] private int _shield;
    [SerializeField] private int _shieldMax;
    [SerializeField] private int _shieldExp;
    [SerializeField] private int _shieldExpMax;
    [SerializeField] private int _shieldStrength;
    [SerializeField] private int _baseAttack;
    [SerializeField] private int _weaponAttack;
    [SerializeField] private bool _isDead;
    private PreviewPlayerData _preview;

    #region プロパティー
    public int Hp { get => _hp; private set => _hp = value; }
    public int HpMax { get => _hpMax; private set => _hpMax = value; }
    public int Exp { get => _exp; private set => _exp = value; }
    public int ExpMax { get => _expmax; private set => _expmax = value; }
    public int Gold { get => _gold; private set => _gold = value; }
    public int GoldMax { get => _goldMax; private set => _goldMax = value; }
    public int Shield { get => _shield; private set => _shield = value; }
    public int ShieldMax { get => _shieldMax; private set => _shieldMax = value; }
    public int ShieldExp { get => _shieldExp; private set => _shieldExp = value; }
    public int ShieldExpMax { get => _shieldExpMax; private set => _shieldExpMax = value; }
    public int ShieldStrength { get => _shieldStrength; private set => _shieldStrength = value; }
    public int BaseAttack { get => _baseAttack; private set => _baseAttack = value; }
    public int WeaponAttack { get => _weaponAttack; private set => _weaponAttack = value; }
    public bool IsDead { get => _isDead; private set => _isDead = value; }
    #endregion

    public void SetResolvePreview(PreviewPlayerData preview)
    {
        _preview = preview;
    }

    /// <summary>
    /// 戦闘処理で計算した結果プレビューを適用する
    /// </summary>
    /// <param name="preview"></param>
    public void ApplyPreview()
    {
        if (_preview == null)
        {
            Debug.LogWarning("プレイヤープレビューデータ無し！！");
            return;
        }
        Hp = _preview.Hp;
        HpMax = _preview.HpMax;
        Exp = _preview.Exp;
        ExpMax = _preview.ExpMax;
        Gold = _preview.Gold;
        GoldMax = _preview.GoldMax;
        Shield = _preview.Shield;
        ShieldMax = _preview.ShieldMax;
        ShieldExp = _preview.ShieldExp;
        ShieldExpMax = _preview.ShieldExpMax;
        ShieldStrength = _preview.ShieldStrength;
        BaseAttack = _preview.BaseAttack;
        WeaponAttack = _preview.WeaponAttack;
        IsDead = _preview.IsDead;
        _preview = null;
    }
}
