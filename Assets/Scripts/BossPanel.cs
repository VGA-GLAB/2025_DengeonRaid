using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEditor;
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
    private void Start()
    {
        EnemyCustomStart();
        UpdateAttrDisplay();
    }
    private void OnDestroy()
    {
        _rm.Igsm.States[typeof(SIGEnemyTurn)].OnExit -= TurnEndBehaviour;
    }
    #endregion
    public override void DestroyThis()
    {
        ReferenceManager.Instance.Igsm.States[typeof(SIGEnemyTurn)].OnExit -= OnTurnEnd;
        _rm.BoardManager.RemovePanelFromBoard(this);
        _rm.GameDirector.BossDefeated();
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

    public override PanelInfo GetPanelInfo()
    {
        string panelName = "ボス";
        string description = "隕石を生み出している張本人。特殊な攻撃もしてくるため注意しよう。";
        StringBuilder attributes = new StringBuilder();
        attributes.AppendLine($"攻撃力:{_attack}");
        attributes.AppendLine($"シールド:{_shield}");
        attributes.AppendLine($"HP:{_hp}");
        return new PanelInfo(panelName, description, attributes.ToString());
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
    /// ボススキル：ランダムのパネルを敵パネルに変換する
    /// </summary>
    private void SkillChangePanels()
    {
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

    protected override void EnemyCustomStart()
    {
        _skills = new List<Action>();
        _skills.Add(() => SkillSelfRecovery());
        _skills.Add(() => SkillChangePanels());

        _excludePanelList = new List<Type>();
        _excludePanelList.Add(typeof(EnemyPanel));
        _excludePanelList.Add(typeof(BossPanel));

        _rm = ReferenceManager.Instance;

        _rm.Igsm.States[typeof(SIGEnemyTurn)].OnExit += OnTurnEnd;
        _rm.Igsm.States[typeof(SIGEnemyTurn)].OnExit += TurnEndBehaviour;
    }
}
