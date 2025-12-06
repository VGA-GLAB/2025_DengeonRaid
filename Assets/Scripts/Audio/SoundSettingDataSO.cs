using UnityEngine;

[CreateAssetMenu(fileName = "SoundSettingDataSO", menuName = "ScriptableObjects/Audio/SoundSettingDataSO")]
/// <summary>
///         音量設定データSO
public class SoundSettingDataSO : ScriptableObject
{
    public float DefaultBGMVolume => _defaultBGMVolume;

    public float DefaultSEVolume => _defaultSEVolume;

    public float MaxBGMVolume => _maxBGMVolume;

    public float MaxSEVolume => _maxSEVolume;

    [Header("デフォルト音量設定")]
    [SerializeField] private float _defaultBGMVolume = 1.0f;
    [SerializeField] private float _defaultSEVolume = 1.0f;

    [Header("最大音量")]
    [SerializeField] private float _maxBGMVolume = 1.0f;
    [SerializeField] private float _maxSEVolume = 1.0f;

    /// <summary>
    ///         BGMの音量読み込みをする。
    /// </summary>
    /// <returns></returns>
    public float LoadBGMVolume()
    {
        return Mathf.Clamp(PlayerPrefs.GetFloat("Audio_BGM_Volume", _defaultBGMVolume), 0f, _maxBGMVolume);
    }

    /// <summary>
    ///         SEの音量読み込みをする。
    /// </summary>
    /// <returns></returns>
    public float LoadSEVolume()
    {
        return Mathf.Clamp(PlayerPrefs.GetFloat("Audio_SE_Volume", _defaultSEVolume), 0f, _maxSEVolume);
    }

    /// <summary>
    ///         BGMの音量保存をする。
    /// </summary>
    /// <param name="volume"></param>
    public void SaveBGMVolume(float volume)
    {
        PlayerPrefs.SetFloat("Audio_BGM_Volume", Mathf.Clamp(volume, 0f, _maxBGMVolume));
        PlayerPrefs.Save();
    }

    /// <summary>
    ///         SEの音量保存をする。
    /// </summary>
    /// <param name="volume"></param>
    public void SaveSEVolume(float volume)
    {
        PlayerPrefs.SetFloat("Audio_SE_Volume", Mathf.Clamp(volume, 0f, _maxSEVolume));
        PlayerPrefs.Save();
    }
}
