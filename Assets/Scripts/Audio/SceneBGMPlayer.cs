using UnityEngine;

public class SceneBGMPlayer : MonoBehaviour
{
    [SerializeField, Header("シーン開始時に再生するBGMの名前")]
    private string _bgmName;

    private async void Start()
    {
        // AudioManagerの読み込み待ち
        await CRIAudioManager.ReadyTask();

        // BGM 再生
        CRIAudioManager.CRIBGMManager.Play(_bgmName);
        // 最初に楽器多め版を再生する
        CRIAudioManager.CRIBGMManager.SelectTrack(Constants.CRI_BGM_SELECTOR_NAME, Constants.CRI_BGM_LABEL_NAME_VER2);
    }
}
