using System;
using System.Collections.Generic;
using UnityEngine;

public class InGameStateMachine
{
    public IInGameState CurrentState; // 現在状態
    public readonly Dictionary<Type, IInGameState> States = new(); // 全ての状態を保持するDictionary、最初に登録する

    public event Action<Type, Type> OnStateChanged; // 状態遷移時呼び出し用delegate。今は未だ使わないかも
    
    /// <summary>
    /// StateMachineで管理されるゲーム状態を登録する
    /// </summary>
    /// <param name="state"></param>
    public void RegisterState(IInGameState state)
    {
        States[state.GetType()] = state;
    }

    /// <summary>
    /// 状態を変更する
    /// </summary>
    /// <typeparam name="T">変更先の状態型</typeparam>
    public void ChangeState<T>() where T : IInGameState
    {
        var type = typeof(T);
        if (!States.ContainsKey(type))
        {
            Debug.LogError($"状態が未登録：{type}");
            return;
        }

        var newState = States[type];
        
        var oldType = CurrentState?.GetType();
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState.Enter();

        OnStateChanged?.Invoke(oldType, type);
    }

    /// <summary>
    /// 現在状態のupdate処理を呼ぶ
    /// </summary>
    public void Update()
    {
        CurrentState?.Update();
    }
}