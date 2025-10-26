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
        //if (Score >= 10)
        //{
        //    SceneManager.LoadScene("03_TestResultScene_Chou");
        //}
        //else
        //{
        _igsm.ChangeState<SIGIdle>();
        //}
    }

    //public void UpdateUIScoreText()
    //{
    //    _uiUpdater.UpdateScoreText();
    //}

    public void PanelResolvingFinished()
    {
        _rm.Igsm.ChangeState<SIGSpawnNewPanel>();
    }
}
