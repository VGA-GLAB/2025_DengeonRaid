using System;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public class BossPanel : EnemyPanel
{
    private List<Action> _skills;
    private int _turnCounter;

    [Header("スキル関連パラメータ"), Space(0.2f)]
    [Header("スキル発動頻度（Xターンに1回）")]
    [SerializeField] private int _skillInterval;
    [Header("回復スキルの回復量")]
    [SerializeField] private int _hpRecovery;
    [Header("敵パネル変換数")]
    [SerializeField] private int _panelAlterCount;

    #region 
    private new void Start()
    {
        UpdateAttrDisplay();

        _skills = new List<Action>();
        _skills.Add(() => SkillSelfRecovery());
        _skills.Add(() => SkillChangePanels());

        ReferenceManager.Instance.Igsm.States[typeof(SIGEnemyTurn)].OnExit += TurnEndBehaviour;
    }
    private void OnDestroy()
    {
        ReferenceManager.Instance.Igsm.States[typeof(SIGEnemyTurn)].OnExit -= TurnEndBehaviour;
    }
    #endregion
    public override void DestroyThis()
    {
        ReferenceManager.Instance.BoardManager.RemovePanelFromBoard(this);
        ReferenceManager.Instance.GameDirector.BossDefeated();
        Destroy(gameObject);
    }
    /// <summary>
    /// ターン終了時の行動。
    /// </summary>
    public void TurnEndBehaviour()
    {
        CountTurn();
        if (_turnCounter >= _skillInterval)
        {
            Action action = PickSkill();
            action.Invoke();
            _turnCounter = 0;
            UpdateAttrDisplay();
        }
    }

    /// <summary>
    /// スキルから1つ選ぶ
    /// </summary>
    /// <returns></returns>
    private Action PickSkill()
    {
        int skillIndex = UnityEngine.Random.Range(0, _skills.Count);
        return _skills[skillIndex];
    }

    /// <summary>
    /// ボススキル：HP回復
    /// </summary>
    private void SkillSelfRecovery()
    {
        // TODO アニメーションかエフェクト
        _hp += _hpRecovery;
    }

    /// <summary>
    /// ボススキル：ランダムのパネルを敵に変換する
    /// </summary>
    private void SkillChangePanels()
    {
        // TODO アニメーションかエフェクト
        Debug.Log("Bossスキル発動：パネル変換");
    }

    private void CountTurn()
    {
        _turnCounter++;
    }
}
