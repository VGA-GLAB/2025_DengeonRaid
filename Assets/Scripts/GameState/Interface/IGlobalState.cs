using System;
using UnityEngine;

public interface IGameState
{
    public void Enter();
    public void Update();
    public void Exit();
}