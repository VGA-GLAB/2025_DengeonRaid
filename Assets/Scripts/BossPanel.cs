using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using Object = System.Object;

public class BossPanel : EnemyPanel
{
    private List<Action> _skills;
    private int _turnCounter;
    private ReferenceManager _rm;

    [Header("スキル関連パラメータ"), Space(0.2f)]
    [Header("スキル用敵Prefab")]
    [SerializeField] private EnemyPanel _enemyPanel;
    [Header("スキル発動頻度（Xターンに1回）")]
    [SerializeField] private int _skillInterval;
    [Header("回復スキルの回復量")]
    [SerializeField] private int _hpRecovery;
    [Header("敵パネル変換数")]
    [SerializeField] private int _panelAlterCount;
    // パネル変換スキルの除外対象
    private List<Type> _excludePanelList;

    #region 
    private new void Start()
    {
        UpdateAttrDisplay();

        _skills = new List<Action>();
        _skills.Add(() => SkillSelfRecovery());
        _skills.Add(() => SkillChangePanels());

        _excludePanelList = new List<Type>();
        _excludePanelList.Add(typeof(EnemyPanel));
        _excludePanelList.Add(typeof(BossPanel));

        _rm = ReferenceManager.Instance;

        _rm.Igsm.States[typeof(SIGEnemyTurn)].OnExit += TurnEndBehaviour;
    }
    private void OnDestroy()
    {
        _rm.Igsm.States[typeof(SIGEnemyTurn)].OnExit -= TurnEndBehaviour;
    }
    #endregion
    public override void DestroyThis()
    {
        _rm.BoardManager.RemovePanelFromBoard(this);
        _rm.GameDirector.BossDefeated();
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
        return _skills[1];
        int skillIndex = UnityEngine.Random.Range(0, _skills.Count);
        return _skills[skillIndex];
    }

    /// <summary>
    /// ボススキル：HP回復
    /// </summary>
    private void SkillSelfRecovery()
    {
        Debug.Log("ボススキル：回復");
        // TODO アニメーションかエフェクト
        _hp += _hpRecovery;
    }

    /// <summary>
    /// ボススキル：ランダムのパネルを敵パネルに変換する
    /// </summary>
    private void SkillChangePanels()
    {
        Debug.Log("ボススキル：パネル変換");
        // TODO アニメーションかエフェクト
        List<Panel> panels = new List<Panel>();
        Func<List<Panel>, Panel, bool> comparer = (panels, panel) => IsPanelInExclusionList(panels, panel);
        // ボードからランダムのパネルを取得する
        for (int i = 0; i < _panelAlterCount; i++)
        {
            panels.Add(_rm.BoardManager.GetRadomPanelExclusive(panels, comparer));
        }
        // 取得したパネルを敵パネルに変換する
        foreach (Panel panel in panels)
        {
            Debug.Log($"Replace Panel : {panel.BoardPos.x}, {panel.BoardPos.y}");
            _rm.BoardManager.ReplacePanel(panel.BoardPos, _enemyPanel);
        }
    }

    /// <summary>
    /// 指定したパネルが変換スキルの除外対象であるか判定する
    /// 除外対象：特定なパネル種類、既に変換しようとするパネル
    /// </summary>
    /// <param name="panel"></param>
    /// <returns></returns>
    private bool IsPanelInExclusionList(List<Panel> panelsToAlter, Panel panel)
    {
        return _excludePanelList.Contains(panel.GetType())
            || panelsToAlter.Contains(panel);
    }

    /// <summary>
    /// 経過ターン数を加算
    /// </summary>
    private void CountTurn()
    {
        _turnCounter++;
    }
}
