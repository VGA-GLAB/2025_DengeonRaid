using System;
using UnityEngine;

/// <summary>
/// InGame状態：パネルをなぞる途中
/// </summary>
public class SIGDrawLine : IInGameState
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
    }

    // 状態終了時に呼び出す処理
    public void Exit()
    {
        OnExit?.Invoke();
    }
}