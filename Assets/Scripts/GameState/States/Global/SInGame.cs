using System;
using UnityEngine;

public class SInGame : IGameState
{
    public InGameStateMachine StateMachine;
    // 状態開始時に呼び出す処理
    public void Enter()
    {
        
    }

    // 状態中のフレーム毎処理
    public void Update()
    {
        
    }

    // 状態終了時に呼び出す処理
    public void Exit()
    {
        
    }

    private void InitInGameStateMachine()
    {
        StateMachine = new InGameStateMachine();
        
    }
}