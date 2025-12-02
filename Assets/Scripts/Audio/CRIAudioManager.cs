using CriWare;
using System;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class CRIAudioManager
{
    public static CRIAudioManager _instance { get; } = new CRIAudioManager();

    public CRIBGMManager BGMManager { get; private set; } = new CRIBGMManager();
    public CRISEManager SEManager { get; private set; } = new CRISEManager();

    private bool _isReady = false;  
    private Queue<Action> _deferredActions = new Queue<Action>();

    public async void InitializeAsync(CriAtomExAcb bgmAcb, CriAtomExAcb seAcb)
    {
        CriAtom atom = GameObject.FindObjectOfType<CriAtom>();
        if (atom == null)
        {
            Debug.LogError("CriAtomがシーン内に存在しない");
            return;
        }
        await Awaitable.WaitForSecondsAsync(0.1f);
    }
}
