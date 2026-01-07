using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///         敵を生成する中ボス
/// </summary>
public class EnemyGenerateMiddleBossPanel : EnemyPanel
{
    [Header("スキル設定")]
    [SerializeField] private int _skillInterval = 3;
    [SerializeField] private int _panelAlterCount = 3;
    [SerializeField] private EnemyPanel _enemyPanel;

    private int _turnCounter;
    private ReferenceManager _rm;
    private List<Type> _excludePanelList;

    protected override void EnemyCustomStart()
    {
        _rm = ReferenceManager.Instance;
        _rm.Igsm.States[typeof(SIGEnemyTurn)].OnExit += OnTurnEnd;

        _excludePanelList = new List<Type>();
        _excludePanelList.Add(typeof(EnemyPanel));
        _excludePanelList.Add(typeof(BossPanel));
    }

    private void OnTurnEnd()
    {
        _turnCounter++;

        if (_turnCounter >= _skillInterval)
        {
            TryGenarateEnemyPanel();
            _turnCounter = 0;
        }
    }

    /// <summary>
    ///         ランダムなパネルを敵に置き換え
    /// </summary>
    private void TryGenarateEnemyPanel()
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

    private void OnDestroy()
    {
        if (_rm != null)
            _rm.Igsm.States[typeof(SIGEnemyTurn)].OnExit -= OnTurnEnd;
    }
}
