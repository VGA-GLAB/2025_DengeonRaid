using NUnit.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EnemyAttackProcessor
{
    private ChouBoardManager _bm;
    private PlayerController _player;
    private PreviewPlayerData _playerPreview;

    public EnemyAttackProcessor(ChouBoardManager bm, PlayerController player)
    {
        _bm = bm;
        _player = player;
        _playerPreview = new PreviewPlayerData(player.Hp, player.Exp, player.Gold, player.Shield, player.ShieldStrength, player.BaseAttack);
    }

    public void Process()
    {
        List<EnemyPanel> enemies = _bm.GetEnemyPanels();

        int enemyAttack = 0;
        foreach (EnemyPanel enemy in enemies)
        {
            enemyAttack += enemy.Attack;
        }

        // シールドが吸収できるダメージ量
        int playerShieldAbsorb = _playerPreview.Shield * _playerPreview.ShieldStrength;
        // 攻撃後に残るシールド値
        int playerShieldRemain = playerShieldAbsorb > enemyAttack ? playerShieldAbsorb - enemyAttack : 0;
        // プレイヤーHPへのダメージ
        int playerHpDamage = playerShieldAbsorb > enemyAttack ? 0 : enemyAttack - playerShieldAbsorb;

        _playerPreview.Hp -= playerHpDamage;
        _playerPreview.Shield = playerShieldRemain;
        if(_playerPreview.Hp <= 0)
        {
            _playerPreview.IsDead = true;
        }

        _player.SetResolvePreview(_playerPreview);
        _player.ApplyPreview();
    }
}
