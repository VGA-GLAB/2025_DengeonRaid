using UnityEngine;
using System.Collections.Generic;

/// <summary>
///         敵への全体攻撃スキル
/// </summary>
public class AttackEnemySkill : SkillBase
{
    [SerializeField] private int _skillDamageRatio = 3;

    public override void ActivateSkill()
    {
        Debug.Log("敵への攻撃スキル発動");

        //  敵パネル全取得
        foreach (EnemyPanel enemy in _boardManager.GetEnemyPanels())
        {
            Debug.Log($"敵パネルID: {enemy} に攻撃を行いました。");
            //  プレビュー計算
            PreviewEnemyData enemyPreview = new PreviewEnemyData(enemy);

            enemyPreview.Hp /= _skillDamageRatio;
            enemy.SetResolvePreview(enemyPreview);
            enemy.ApplyPreview();
            enemy.UpdateAttrDisplay();

            if (enemy.Hp <= 0)
            {
                Debug.Log($"敵パネルID: {enemy} を撃破しました。");
                _boardManager.DeleatePanel(enemy);
            }
        }

        _boardManager.ForceDropPanel();
    }
}
