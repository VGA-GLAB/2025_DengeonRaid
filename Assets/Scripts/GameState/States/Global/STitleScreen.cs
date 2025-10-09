using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class STitleScreen : IGameState
{
    // 状態開始時に呼び出す処理
    public void Enter()
    {
        // BGM再生、UI初期表示などを行う
        AudioManager.Instance.PlayBgm("TitleScreen");
    }

    // 状態中のUpdate処理
    public void Update()
    {
    }

    // 状態終了時に呼び出す処理
    public void Exit()
    {
    }
}