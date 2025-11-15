using NUnit.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EnemyAttackProcessor
{
    private BoardManager _bm;
    private PlayerController _player;
    private PreviewPlayerData _playerPreview;
    public int FinalDamage { get; private set; }

    public EnemyAttackProcessor(BoardManager bm, PlayerController player)
    {
        _bm = bm;
        _player = player;
        _playerPreview = new PreviewPlayerData(_player);
    }

    /// <summary>
    /// ボードに敵パネルがあるか
    /// </summary>
    /// <returns></returns>
    public bool EnemyExists()
    {
        List<EnemyPanel> enemies = _bm.GetEnemyPanels();
        return enemies != null && enemies.Count > 0;
    }

    /// <summary>
    /// 敵攻撃のダメージを算出する
    /// </summary>
    public void Process()
    {
        List<EnemyPanel> enemies = _bm.GetEnemyPanels();

        if(enemies.Count == 0)
        {
            return;
        }

        int enemyAttack = 0;
        foreach (EnemyPanel enemy in enemies)
        {
            enemyAttack += enemy.Attack;
        }

        // シールドが吸収できるダメージ量
        int playerShieldAbsorb = _playerPreview.Shield * _playerPreview.ShieldStrength;
        // 攻撃後に残るシールド値
        int playerShieldRemain = enemyAttack > playerShieldAbsorb ? 0 : (playerShieldAbsorb - enemyAttack) / _playerPreview.ShieldStrength;
        // プレイヤーHPへのダメージ
        int playerHpDamage = playerShieldAbsorb > enemyAttack ? 0 : enemyAttack - playerShieldAbsorb;
        FinalDamage = playerHpDamage;

        _playerPreview.Hp -= playerHpDamage;
        _playerPreview.Shield = playerShieldRemain;
        if(_playerPreview.Hp <= 0)
        {
            _playerPreview.IsDead = true;
        }

        _player.SetResolvePreview(_playerPreview);
    }

    /// <summary>
    /// 算出した結果をプレイヤーに適用する
    /// </summary>
    public void ApplyDamageToPlayer()
    {
        _player.ApplyPreview();
    }
}
