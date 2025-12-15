/// <summary>
///         敵が死亡するかどうかを判定するユーティリティクラス
/// </summary>
public static class EnemyDeathCheckUtility
{
    /// <summary>
    ///         敵が死亡するかどうかを判定する
    /// </summary>
    public static bool EnemyDeathCheck(EnemyPanel enemy, PlayerController player, int playerWeaponAmount)
    {
        // プレイヤーの総攻撃力
        int playerAttack = player.BaseAttack + (player.WeaponAttack * playerWeaponAmount);
        // 敵のシールドが吸収できるダメージ量
        int enemyShieldAbsorb = enemy.Shield * enemy.ShieldStrength;
        // 敵が攻撃を受けて残るシールド数
        int enemyShieldRemain = playerAttack > enemyShieldAbsorb ? 0 : (enemyShieldAbsorb - playerAttack) / enemy.ShieldStrength;
        // 敵Hpへのダメージ
        int enemyHpDamage = playerAttack > enemyShieldAbsorb ? playerAttack - enemyShieldAbsorb : 0;

        bool isdead = enemy.Hp - enemyHpDamage <= 0;

        return isdead;
    }
}
