using CriWare;
using System;
using UnityEngine;

/// <summary>
///         単一BGMの管理クラス
/// </summary>
public class CRIBGMManager
{
    public CRIBGMManager(CriAtomExAcb criAtomExAcb)
    {
        // 初期化
        _criAtomExAcb = criAtomExAcb;
        if (_player == null) _player = new CriAtomExPlayer();
        SetVolume(SoundSettings.BGMVolume);
    }

    private readonly CriAtomExPlayer _player;
    private readonly CriAtomExAcb _criAtomExAcb;

    /// <summary>
    ///         CriAtomExPlayerに音量を設定
    /// </summary>
    /// <param name="volume">SoundSettingsに設定している音量</param>
    public void SetVolume(float volume)
    {
        if (_player != null)
        {
            _player.SetVolume(Mathf.Clamp01(volume));
            _player.UpdateAll();
        }
    }

    /// <summary>
    ///         BGMの再生
    /// </summary>
    /// <param name="cueName">再生するBGMの名前</param>
    public void Play(string cueName)
    {
        if (_criAtomExAcb == null) return;

        _player.Stop();

        // CriAtomExから音楽を探して再生
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

    /// <summary>
    ///         BGMを切り替える
    /// </summary>
    /// <param name="selector"></param>
    /// <param name="label"></param>
    public void SelectTrack(string selector, string label)
    {
        _player.SetSelectorLabel(selector, label);
        _player.UpdateAll();
    }
}
