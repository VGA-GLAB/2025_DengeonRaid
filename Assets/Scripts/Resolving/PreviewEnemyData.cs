using UnityEngine;
/// <summary>
/// パネル消去後の結果プレビューデータ_敵パネル側
/// </summary>
public class PreviewEnemyData
{
    public int Hp;
    public int Shield;
    public int ShieldStrength;
    public int Attack;
    public int KillExp;
    public bool IsDead = false;

    public PreviewEnemyData(EnemyPanel enemy)
    {
        this.Hp = enemy.Hp;
        this.Shield = enemy.Shield;
        this.ShieldStrength = enemy.ShieldStrength;
        this.KillExp = enemy.KillExp;
        this.Attack = enemy.Attack;
    }
}
