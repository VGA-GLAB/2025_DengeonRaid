using CriWare;
using System.Collections.Generic;
using UnityEngine;

public class CRISEManager
{
    private List<SEPlayer> _pool = new List<SEPlayer>();
    private CriAtomExAcb _criAtomExAcb;
    private int _poolSize = 10;

    public void Initialize(CriAtomExAcb criAtomExAcb)
    {
        _criAtomExAcb = criAtomExAcb;

        for (int i = 0; i < _poolSize; i++)
        {
            _pool.Add(new SEPlayer(_criAtomExAcb));
        }
    }

    public void SetVolome(float volume)
    {
        foreach (var p in _pool)
        {
            p?.SetVolume(volume);
        }
    }

    /// <summary>
    ///         
    /// </summary>
    /// <param name="cueName"></param>
    public void PlaySEDesignation(string cueName)
    {
        SEPlayer free = _pool.Find(p => !p.IsPlaying);
        if (free != null)
            free.Play(cueName);
    }

    public void DisposeAll()
    {
        foreach (var p in _pool)
        {
            p?.Dispose();
        }
        _pool.Clear();
    }
}
