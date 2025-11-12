using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// InGame状態：ターンエンド
/// </summary>
public class SIGTurnEnd : IInGameState
{
    public event Action OnEnter;
    public event Action OnUpdate;
    public event Action OnExit;
    // 状態開始時に呼び出す処理
    public void Enter()
    {
        OnEnter?.Invoke();
    }

    // 状態中のフレーム毎処理
    public void Update()
    {
        OnUpdate?.Invoke();
    }

    // 状態終了時に呼び出す処理
    public void Exit()
    {
        OnExit?.Invoke();
    }
}