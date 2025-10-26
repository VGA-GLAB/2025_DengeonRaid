using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int _hp;
    [SerializeField] private int _exp;
    [SerializeField] private int _gold;
    [SerializeField] private int _shield;
    [SerializeField] private int _shieldStrength;
    [SerializeField] private int _baseAttack;
    [SerializeField] private bool _isDead;
    private PreviewPlayerData _preview;

    public int Hp { get => _hp; private set => _hp = value; }
    public int Exp { get => _exp; private set => _exp = value; }
    public int Gold { get => _gold; private set => _gold = value; }
    public int Shield { get => _shield; private set => _shield = value; }
    public int ShieldStrength { get => _shieldStrength; private set => _shieldStrength = value; }
    public int BaseAttack { get => _baseAttack; private set => _baseAttack = value; }
    public bool IsDead { get => _isDead; private set => _isDead = value; }

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
        Exp = _preview.Exp;
        Gold = _preview.Gold;
        Shield = _preview.Shield;
        ShieldStrength = _preview.ShieldStrength;
        BaseAttack = _preview.BaseAttack;
        _preview = null;
    }
}
