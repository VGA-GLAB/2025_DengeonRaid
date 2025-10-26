using UnityEngine;

/// <summary>
/// パネル消去後の結果プレビューデータ_プレイヤー側
/// </summary>
public class PreviewPlayerData
{
    public int Hp = 0;
    public int Exp = 0;
    public int Gold = 0;
    public int Shield = 0;
    public int ShieldStrength = 0;
    public int PanelAttack = 0;
    public int BaseAttack = 0;
    public bool IsDead = false;

    public PreviewPlayerData(int hp, int exp, int gold, int shield, int shieldStrength, int baseAttack)
    {
        Hp = hp;
        Exp = exp;
        Gold = gold;
        Shield = shield;
        ShieldStrength = shieldStrength;
        PanelAttack = 0;
        BaseAttack = baseAttack;
    }
}