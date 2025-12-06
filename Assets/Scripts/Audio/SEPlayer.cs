using CriWare;
using System;

/// <summary>
///         SE一つを再生するクラス
/// </summary>
public class SEPlayer
{
    public SEPlayer(CriAtomExAcb criAtomExAcb)
    {
        _criAtomExAcb = criAtomExAcb;
        _player = new CriAtomExPlayer();
        _player.SetVolume(SoundSettings.SEVolume);
    }

    private CriAtomExPlayer _player;
    private CriAtomExAcb _criAtomExAcb;

    public bool IsPlaying => _player != null && _player.GetStatus() == CriAtomExPlayer.Status.Playing;

    /// <summary>
    ///         CriAtomExPlayerに音量を設定
    /// </summary>
    /// <param name="volume">SoundSettingsに設定している音量</param>
    public void SetVolume(float volume)
    {
        _player?.SetVolume(volume);
    }

    /// <summary>
    ///         SEの再生
    /// </summary>
    /// <param name="cueName"></param>
    public void Play(string cueName)
    {
        if (_criAtomExAcb == null) return;

        CriAtomEx.CueInfo[] cueArray = _criAtomExAcb.GetCueInfoList();
        CriAtomEx.CueInfo info = Array.Find(cueArray, ci => ci.name == cueName);
        _player.SetCue(_criAtomExAcb, info.id);
        _player.Start();
    }

    /// <summary>
    ///         再生を停止 & CriAtomExPlayerを破棄
    ///         Stopの必要がないのでこうしてます
    /// </summary>
    public void Dispose()
    {
        if (_player == null) return;

        _player.Stop();
        _player.Dispose();
        _player = null;
    }
}
