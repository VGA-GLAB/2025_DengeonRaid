using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

/// <summary>
/// ゲーム全体の調達をする立場
/// </summary>
public class GameDirector : MonoBehaviour
{
    private ReferenceManager _rm;
    private InGameStateMachine _igsm;
    private EventBus _eventBus;
    private PlayerController _player;
    private bool _bossDefeated = false;
    private bool _bossExists = false;

    public bool BossExists { get => _bossExists; private set => _bossExists = value; }

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
        _player = _rm.PlayerController;
        // TODO 本番ではSIGIntro
        _igsm.ChangeState<SIGIdle>();
        _igsm.States[typeof(SIGShop)].OnEnter += CheckOpenShop;
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
        _igsm.RegisterState(new SIGTurnEnd());
        _igsm.RegisterState(new SIGShop());
        _igsm.RegisterState(new SIGLevelUp());
        _igsm.RegisterState(new SIGBusy());
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
        StartCoroutine(PanelResolvingFinishedSequence());
    }

    private IEnumerator PanelResolvingFinishedSequence()
    {
        // TODO レベルアップシステム未実装
        //_igsm.ChangeState<SIGLevelUp>();
        //yield return new WaitUntil(() => !(_igsm.CurrentState is SIGLevelUp));
        _igsm.ChangeState<SIGShop>();
        yield return new WaitUntil(() => !(_igsm.CurrentState is SIGShop));
        _igsm.ChangeState<SIGSpawnNewPanel>();
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
    /// <summary>
    /// レベルアップ完了
    /// </summary>

    public void LevelUpFinished()
    {
        _igsm.ChangeState<SIGBusy>();
    }

    /// <summary>
    /// ショップ完了
    /// </summary>
    public void ShopFinished()
    {
        _igsm.ChangeState<SIGBusy>();
    }

    /// <summary>
    /// ショップの出現条件を判定し、出現処理を行う
    /// </summary>
    public void CheckOpenShop()
    {
        if (_player.CheckEnterShop())
        {
            _player.ProcessEnterShop();
            _rm.ShopUIManager.OpenShop();
        }
        else
        {
            _igsm.ChangeState<SIGBusy>();
        }
    }
}
