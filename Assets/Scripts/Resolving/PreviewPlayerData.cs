using UnityEngine;

/// <summary>
/// パネル消去後の結果プレビューデータ_プレイヤー側
/// </summary>
public class PreviewPlayerData
{
    public int HpMax;
    public int Hp;
    public int Exp;
    public int ExpMax;
    public int Money;
    public int MoneyMax;
    public int Shield;
    public int ShieldMax;
    public int ShieldExp;
    public int ShieldExpMax;
    public int ShieldStrength;
    public int BaseAttack;
    public int WeaponAttack;
    public int WeaponAmount;
    public bool IsDead;


    public PreviewPlayerData(PlayerController player)
    {
        Hp = player.Hp;
        HpMax = player.HpMax;
        Exp = player.Exp;
        ExpMax = player.ExpMax;
        Money = player.Money;
        MoneyMax = player.MoneyMax;
        Shield = player.Shield;
        ShieldMax = player.ShieldMax;
        ShieldExp = player.ShieldExp;
        ShieldExpMax = player.ShieldExpMax;
        ShieldStrength = player.ShieldStrength;
        BaseAttack = player.BaseAttack;
        WeaponAttack = player.WeaponAttack;
        WeaponAmount = 0;
        IsDead = player.IsDead;
    }
}