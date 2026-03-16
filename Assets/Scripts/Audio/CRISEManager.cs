using CriWare;
using System.Collections.Generic;

/// <summary>
///         SEの再生を統括するクラス
/// </summary>
public class CRISEManager
{
    public CRISEManager(CriAtomExAcb criAtomExAcb) 
    {
        _criAtomExAcb = criAtomExAcb;

        for (int i = 0; i < _poolSize; i++)
        {
            _pool.Add(new SEPlayer(_criAtomExAcb));
        }
    }

    private List<SEPlayer> _pool = new();
    private CriAtomExAcb _criAtomExAcb;
    private int _poolSize = 30;

    /// <summary>
    ///         音量を設定
    /// </summary>
    /// <param name="volume"></param>
    public void SetVolume(float volume)
    {
        foreach (var p in _pool)
        {
            p?.SetVolume(volume);
        }
    }

    /// <summary>
    ///         指定したSEの再生
    /// </summary>
    /// <param name="cueName"></param>
    public void Play(string cueName)
    {
        SEPlayer free = _pool.Find(p => !p.IsPlaying);
        if (free != null)
            free.Play(cueName);
    }

    /// <summary>
    ///         指定したSEを再生し、制御用プレイヤーも取得する
    /// </summary>
    /// <param name="cueName"></param>
    /// <returns></returns>
    public SEPlayer PlayAndGetPlayer(string cueName)
    {
        SEPlayer free = _pool.Find(p => !p.IsPlaying);
        if (free != null)
            free.Play(cueName);
        return free;
    }

    /// <summary>
    ///         フェードアウト付きで指定したSEを再生
    /// </summary>
    /// <param name="cueName"></param>
    /// <param name="fadeTime"></param>
    /// <returns></returns>
    public SEPlayer PlayFadable(string cueName, int fadeTime)
    {
        SEPlayer free = _pool.Find(p => !p.IsPlaying);
        if (free != null)
            free.PlayFadable(cueName, fadeTime);
        return free;
    }

    /// <summary>
    ///         指定したSEを停止する
    /// </summary>
    /// <param name="player"></param>
    public void Stop(SEPlayer player)
    {
        player.Stop();
    }

    /// <summary>
    ///         すべてのSEを止める
    /// </summary>
    public void DisposeAll()
    {
        foreach (var p in _pool)
        {
            p?.Dispose();
        }
        _pool.Clear();
    }
}
