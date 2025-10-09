using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameStateManager : MonoBehaviour
{
    public static InGameStateManager Instance;
    /// <summary>InGame StateMachine</summary>
    public InGameStateMachine IGsm;
    public int Score = 0;
    
    [SerializeField] private PanelMovement _panelMovement;
    [SerializeField] private UIUpdater _uiUpdater;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        InitGlobalStateMachine();
    }

    private void Start()
    {
        // 状態のeventに処理を登録する
        IGsm.States[typeof(SIGIntro)].OnEnter += UpdateUIScoreText;
        IGsm.States[typeof(SIGCheckGameEnd)].OnEnter += JudgeGameEnd;
        IGsm.States[typeof(SIGEliminatePanel)].OnEnter += UpdateUIScoreText;
        IGsm.ChangeState<SIGIntro>();
    }

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

    public void JudgeGameEnd()
    {
        if (Score >= 10)
        {
            SceneManager.LoadScene("03_TestResultScene_Chou");
        }
        else
        {
            IGsm.ChangeState<SIGIdle>();
        }
    }

    public void UpdateUIScoreText()
    {
        _uiUpdater.UpdateScoreText();
    }
}