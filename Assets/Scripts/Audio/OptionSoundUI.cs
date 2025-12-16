using UnityEngine;
using UnityEngine.UI;

public class OptionSoundUI : MonoBehaviour
{
    [Header("UI参照")]
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider seSlider;

    private bool _initialized = false;

    private void Start()
    {
        // 保存された値を反映
        bgmSlider.value = SoundSettings.BGMVolume;
        seSlider.value = SoundSettings.SEVolume;

        // スライダー変更時の処理を登録
        bgmSlider.onValueChanged.AddListener(HandleBGMValueChanged);
        seSlider.onValueChanged.AddListener(HandleSEValueChanged);

        _initialized = true;
    }

    /// <summary>
    ///         BGM 音量変更
    /// </summary>
    private void HandleBGMValueChanged(float value)
    {
        if (!_initialized) return;

        if (CRIAudioManager.Instance != null && CRIAudioManager.Instance.IsReady)
        {
            // 保存と反映
            CRIAudioManager.SoundSettings.SaveBGMVolume(value);
            CRIAudioManager.CRIBGMManager.SetVolume(value);
        }
    }

    /// <summary>
    ///         SE 音量変更
    /// </summary>
    private void HandleSEValueChanged(float value)
    {
        if (!_initialized) return;

        // 反映
        if (CRIAudioManager.Instance != null && CRIAudioManager.Instance.IsReady)
        {
            CRIAudioManager.SoundSettings.SaveSEVolume(value);
            CRIAudioManager.CRISEManager.SetVolume(value);
        }
    }
}
