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
    public bool IsDead = false;

    public PreviewEnemyData(int Hp, int Shield, int ShieldStrength, int Attack)
    {
        this.Hp = Hp;
        this.Shield = Shield;
        this.ShieldStrength = ShieldStrength;
        this.Attack = Attack;
    }

    public　PreviewEnemyData(EnemyPanel enemy)
    {
        this.Hp = enemy.Hp;
        this.Shield = enemy.Shield;
        this.ShieldStrength = enemy.ShieldStrength;
        this.Attack = enemy.Attack;
        this.IsDead = enemy.IsDead;
    }
}
