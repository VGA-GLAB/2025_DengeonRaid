using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲーム全体の調達をする立場
/// </summary>
public class GameDirector : MonoBehaviour
{
    private ReferenceManager _rm;
    private InGameStateMachine _igsm;
    private EventBus _eventBus;

    #region ライフサイクル
    private void Awake()
    {
        _rm = ReferenceManager.Instance;

        InitInGameStateMachine();
        _eventBus = new EventBus();
        _rm.EventBus = _eventBus;
        _rm.Igsm = _igsm;
        _igsm.States[typeof(SIGCheckGameEnd)].OnEnter += JudgeGameEnd;

    }

    private void Start()
    {
        // TODO 本番ではSIGIntro
        _igsm.ChangeState<SIGIdle>();
    }
    #endregion

    private void InitInGameStateMachine()
    {
        _igsm = new InGameStateMachine();
        _igsm.RegisterState(new SIGIntro());
        _igsm.RegisterState(new SIGIdle());
        _igsm.RegisterState(new SIGDrawLine());
        _igsm.RegisterState(new SIGEliminatePanel());
        _igsm.RegisterState(new SIGSpawnNewPanel());
        _igsm.RegisterState(new SIGCheckGameEnd());
        _igsm.RegisterState(new SIGEnemyTurn());
    }

    /// <summary>
    /// ゲーム終了条件を満たしたか判定する
    /// 満たした場合、ゲーム終了関連の処理を行う
    /// </summary>
    private void JudgeGameEnd()
    {
        // TODO ゲーム終了判定処理
        _igsm.ChangeState<SIGIdle>();
    }

    /// <summary>
    /// パネル消去処理完了
    /// </summary>
    public void PanelResolvingFinished()
    {
        _rm.Igsm.ChangeState<SIGSpawnNewPanel>();
    }

    /// <summary>
    /// パネル生成処理完了
    /// </summary>
    public void SpawnNewPanelFinished()
    {
        _rm.Igsm.ChangeState<SIGEnemyTurn>();
    }

    /// <summary>
    /// 敵攻撃処理完了
    /// </summary>
    public void EnemyAttackFinished()
    {
        _rm.Igsm.ChangeState<SIGIdle>();
    }
}
