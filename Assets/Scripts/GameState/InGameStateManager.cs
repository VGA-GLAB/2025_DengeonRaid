using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameStateManager : StaticInstanceMonoBehaviour<InGameStateManager>
{
    /// <summary>InGame StateMachine</summary>
    public InGameStateMachine IGsm;
    public int Score = 0;
    
    [SerializeField] private PanelMovement _panelMovement;
    //[SerializeField] private UIController uiController;

    #region ライフサイクル
    private void Start()
    {
        // 状態のeventに処理を登録する
        IGsm.States[typeof(SIGCheckGameEnd)].OnEnter += JudgeGameEnd;
        IGsm.ChangeState<SIGIdle>();
    }
    #endregion

    /// <summary>
    /// 設定を書く感じで、ゲームで使用する状態を登録する
    /// </summary>
    private void InitGlobalStateMachine()
    {
        IGsm = new InGameStateMachine();
        IGsm.RegisterState(new SIGIntro());
        IGsm.RegisterState(new SIGIdle());
        IGsm.RegisterState(new SIGDrawLine());
        IGsm.RegisterState(new SIGEliminatePanel());
        IGsm.RegisterState(new SIGSpawnNewPanel());
        IGsm.RegisterState(new SIGBusy());
        IGsm.RegisterState(new SIGCheckGameEnd());
    }

    protected override void CustomAwake()
    {
        InitGlobalStateMachine();
    }

    public void JudgeGameEnd()
    {
        //if (Score >= 10)
        //{
        //    SceneManager.LoadScene("03_TestResultScene_Chou");
        //}
        //else
        //{
            IGsm.ChangeState<SIGIdle>();
        //}
    }

    //public void UpdateUIScoreText()
    //{
    //    _uiUpdater.UpdateScoreText();
    //}
}