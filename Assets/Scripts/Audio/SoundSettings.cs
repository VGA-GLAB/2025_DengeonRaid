using UnityEngine;

/// <summary>
///         音量を保存、取得するユーティリティクラス
///         最大音量を1として01で制御
/// </summary>
public static class SoundSettings
{
    private const string KEY_BGM = "Audio_BGM_Volume";
    private const string KEY_SE = "Aduio_SE_Volume";

    public static float BGMVolume
    {
        get => PlayerPrefs.GetFloat(KEY_BGM, 1.0f);
        set
        {
            PlayerPrefs.SetFloat(KEY_BGM, Mathf.Clamp01(value));
            PlayerPrefs.Save();
        }
    }

    public static float SEVolume
    {
        get => PlayerPrefs.GetFloat(KEY_SE, 1.0f);
        set
        {
            PlayerPrefs.SetFloat(KEY_SE, Mathf.Clamp01(value));
            PlayerPrefs.Save();
        }
    }
}
