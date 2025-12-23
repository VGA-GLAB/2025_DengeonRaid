using UnityEngine;

/// <summary>
///         ワープする中ボス
/// </summary>
public class WarpingMiddleBossPanel : EnemyPanel
{
    [Header("スキル設定"), SerializeField]
    private int _skillInterval = 3;

    private int _turnCounter;
    private ReferenceManager _rm;

    protected override void EnemyCustomStart()
    {
        _rm = ReferenceManager.Instance;
        _rm.Igsm.States[typeof(SIGEnemyTurn)].OnExit += OnTurnEnd;
    }

    private void OnDestroy()
    {
        if (_rm != null)
            _rm.Igsm.States[typeof(SIGEnemyTurn)].OnExit -= OnTurnEnd;
    }

    private void OnTurnEnd()
    {
        _turnCounter++;

        if (_turnCounter >= _skillInterval)
        {
            TrySwapRandomPanel();
            _turnCounter = 0;
        }
    }

    /// <summary>
    ///         ランダムなパネルと自信を入れ替える
    /// </summary>
    private void TrySwapRandomPanel()
    {
        Panel target = null;

        // 自分以外がひけるように複数回ループを回す
        for (int i = 0; i < 10; i++)
        {
            Panel panel = _rm.BoardManager.GetRandomPanelFromBoard();

            if (panel == null || panel == this) continue;

            target = panel;
            break;
        }

        _rm.BoardManager.SwapPanels(this, target);
        // 演出入れるならここになるかも
    }
}
