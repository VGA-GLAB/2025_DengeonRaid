using CriWare;
using UnityEngine;
using System;

/// <summary>
///         単一BGMの管理クラス
/// </summary>
public class CRIBGMManager
{
    private CriAtomExPlayer _player;
    private CriAtomExAcb _criAtomExAcb;

    /// <summary>
    ///         初期化
    /// </summary>
    /// <param name="criAtomExAcb"></param>
    public void Initialize(CriAtomExAcb criAtomExAcb)
    {
        _criAtomExAcb = criAtomExAcb;
        if (_player == null) _player = new CriAtomExPlayer();
        SetVolume(SoundSettings.BGMVolume);
    }

    /// <summary>
    ///         CriAtomExPlayerに音量を設定
    /// </summary>
    /// <param name="volume">SoundSettingsに設定している音量</param>
    public void SetVolume(float volume)
    {
        if (_player != null) _player.SetVolume(Mathf.Clamp01(volume));
    }

    /// <summary>
    ///         BGMの再生
    /// </summary>
    /// <param name="cueName">再生するBGMの名前</param>
    public void Play(string cueName)
    {
        if (_criAtomExAcb == null) return;

        _player.Stop();

        // CriAtomExから長く音楽を探して再生
        CriAtomEx.CueInfo[] cueArray = _criAtomExAcb.GetCueInfoList();
        CriAtomEx.CueInfo info = Array.Find(cueArray, ci => ci.name == cueName);
        _player.SetCue(_criAtomExAcb, info.id);
        _player.Start();
    }

    /// <summary>
    ///         BGMの停止
    /// </summary>
    public void Stop()
    {
        _player?.Stop();
    }
}
