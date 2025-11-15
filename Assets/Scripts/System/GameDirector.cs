using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲーム全体の調達をする立場
/// </summary>
public class GameDirector : MonoBehaviour
{
    private ReferenceManager _rm;
    private InGameStateMachine _igsm;
    private EventBus _eventBus;
    private PlayerController _player;
    private bool _playerDefeated;
    private bool _bossDefeated;
    private bool _bossExists;
    private int _enemyCount;
    [SerializeField, Header("ボスの出現条件（敵撃破数）")] private int _enemyCountForBoss;
    [SerializeField, Header("デバッグ用　現在STATE"), ReadOnly] public string CurrentState;

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

        _enemyCount = 0;
        _bossDefeated = false;
        _bossExists = false;
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
        _igsm.RegisterState(new SIGPending());
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
        if(_bossDefeated)
        {
            _rm.UIController.WipeIn(LoadWinResultScene);
            yield break;
        }
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
        if (_rm.PlayerController.IsDead)
        {
            _rm.UIController.WipeIn(LoadLoseResultScene);
        }
    }
    /// <summary>
    /// レベルアップ完了
    /// </summary>

    public void LevelUpFinished()
    {
        _igsm.ChangeState<SIGPending>();
    }

    /// <summary>
    /// ショップ完了
    /// </summary>
    public void ShopFinished()
    {
        _igsm.ChangeState<SIGPending>();
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

    /// <summary>
    /// 敵撃破数を累計する
    /// </summary>
    /// <param name="count"></param>
    public void CountEnemyKill(int count)
    {
        _enemyCount += count;
    }
    /// <summary>
    /// ボスを生成する条件を達しているか
    /// </summary>
    /// <returns></returns>
    public bool CanGenerateBoss()
    {
        return _enemyCount >= _enemyCountForBoss && (!_bossExists);
    }

    public void BossGenerated()
    {
        _bossExists = true;
    }
    /// <summary>
    /// ボス撃破
    /// </summary>
    public void BossDefeated()
    {
        _bossDefeated = true;
    }

    /// <summary>
    /// プレイヤー敗北
    /// </summary>
    public void PlayerDefeated()
    {
        _rm.UIController.WipeIn(LoadLoseResultScene);
    }

    /// <summary>
    /// リザルトシーンに遷移＿勝利
    /// </summary>
    private void LoadWinResultScene()
    {
        SceneManager.LoadScene("ResultWin");
    }

    /// <summary>
    /// リザルトシーンに遷移＿敗北
    /// </summary>
    private void LoadLoseResultScene()
    {
        SceneManager.LoadScene("ResultLose");
    }
}
