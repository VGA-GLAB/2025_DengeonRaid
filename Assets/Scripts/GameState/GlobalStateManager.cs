using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalStateManager : MonoBehaviour
{
    public static GlobalStateManager Instance;
    /// <summary>全体StateMachine</summary>
    public GlobalStateMachine Gsm;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        InitGlobalStateMachine();
    }

    private void Start()
    {
        Gsm.ChangeState<STitleScreen>();
    }

    private void InitGlobalStateMachine()
    {
        Gsm = new GlobalStateMachine();
        Gsm.RegisterState(new STitleScreen());
        Gsm.RegisterState(new SInGame());
        Gsm.RegisterState(new SResultScreen());
    }

    public void OnStartButtonClick()
    {
        Gsm.ChangeState<SInGame>();
        SceneManager.LoadScene("02_TestInGameScene_Chou");
    }
}