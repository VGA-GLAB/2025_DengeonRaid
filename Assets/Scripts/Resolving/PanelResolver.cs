using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class PanelResolver
{
    private PlayerController _player;
    private PreviewPlayerData _playerPreview;
    private Stack<Panel> _panels;
    private List<EnemyPanel> _enemies;

    #region Publicメソッド
    public PanelResolver(PlayerController player, Stack<Panel> panels)
    {
        _player = player;
        _panels = panels;
        _playerPreview = new PreviewPlayerData(_player);
        _enemies = new List<EnemyPanel>();
    }

    /// <summary>
    /// パネル消去結果のプレビューデータを作成する
    /// </summary>
    public void ProcessPreview()
    {
        foreach (var panel in _panels)
        {
            if (!(panel is EnemyPanel))
            {
                // 消去パネルが敵以外の場合、パネル効果をプレイヤープレビューに反映する
                panel.Effect(_playerPreview);
            }
            else
            {
                // 消去パネルが敵の場合、敵リストに追加しておく
                _enemies.Add((EnemyPanel)panel);
            }
        }

        // 敵リストが空でない場合、敵ごとにプレイヤーから攻撃を行う
        if (_enemies.Count > 0)
        {
            foreach (var enemy in _enemies)
            {
                PreviewEnemyData enemyPreview = new PreviewEnemyData(enemy);
                ProcessPlayerAttack(enemyPreview);
                enemy.SetResolvePreview(enemyPreview);
            }
        }

        _player.SetResolvePreview(_playerPreview);
    }

    /// <summary>
    /// 作成したプレビューを適用する
    /// </summary>
    public void ApplyPreviews()
    {
        foreach (var enemy in _enemies)
        {
            enemy.ApplyPreview();
        }
        foreach(Panel panel in _panels)
        {
            if(panel is EnemyPanel)
            {
                EnemyPanel enemy = panel as EnemyPanel;
                if (enemy.IsDead)
                {
                    enemy.DestroyThis();
                }
                else
                {
                    enemy.UpdateAttrDisplay();
                }
            }
            else
            {
                panel.DestroyThis();
            }
        }
        _player.ApplyPreview();
    }
    #endregion
    /// <summary>
    /// プレイヤーから敵への攻撃を実行し、敵のプレビューデータを設定する
    /// </summary>
    /// <param name="player"></param>
    /// <param name="playerPreview"></param>
    /// <param name="enemyPreview"></param>
    private void ProcessPlayerAttack(PreviewEnemyData enemyPreview)
    {
        // プレイヤーの総攻撃力
        int playerAttack = _player.BaseAttack + (_playerPreview.WeaponAttack * _playerPreview.WeaponAmount);
        // 敵のシールドが吸収できるダメージ量
        int enemyShieldAbsorb = enemyPreview.Shield * enemyPreview.ShieldStrength;
        // 敵が攻撃を受けて残るシールド数
        int enemyShieldRemain = playerAttack > enemyShieldAbsorb ? 0 : (enemyShieldAbsorb - playerAttack) / enemyPreview.ShieldStrength;
        // 敵Hpへのダメージ
        int enemyHpDamage = playerAttack > enemyShieldAbsorb ? playerAttack - enemyShieldAbsorb : 0;

        enemyPreview.Shield = enemyShieldRemain;
        enemyPreview.Hp -= enemyHpDamage;
        if(enemyPreview.Hp <= 0)
        {
            _playerPreview.Exp += enemyPreview.KillExp;
            enemyPreview.IsDead = true;
        }
    }
}
