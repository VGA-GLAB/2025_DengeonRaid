using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private SerializedDictionary<string, AudioClip> _bgmClips;
    [SerializeField] private SerializedDictionary<string, AudioClip> _soundClips;
    private AudioSource _audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Init();
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void Init()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// BGM名を指定して再生する
    /// </summary>
    /// <param name="bgmName"></param>
    public void PlayBgm(string bgmName)
    {
        if (!_bgmClips.ContainsKey(bgmName))
        {
            Debug.LogWarning($"Clip「{bgmName}」が存在しません。");
            return;
        }
        AudioClip clip = _bgmClips[bgmName];
        _audioSource.clip = clip;
        _audioSource.Play();
    }
    
    /// <summary>
    /// SE名を指定して1回再生する
    /// </summary>
    /// <param name="soundName"></param>
    public void PlaySound(string soundName)
    {
        if (_soundClips.ContainsKey(soundName))
        {
            Debug.LogWarning($"Clip「{soundName}」が存在しません。");
            return;
        }
        AudioClip clip = _soundClips[soundName];
        _audioSource.PlayOneShot(clip);
    }
}