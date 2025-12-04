using UnityEngine;
using Cysharp.Threading.Tasks;

public class TestAudioPlayer : MonoBehaviour
{
    private async void Start()
    {
        // AudioManagerの読み込み待ち
        await UniTask.WaitUntil(() => CRIAudioManager.Instance.IsReady);

        Debug.Log("AudioManager Ready!");

        // BGM 再生
        CRIAudioManager.Instance.BGMManager.Play("ME_Gameover");

        // 1秒後に SE 再生
        await UniTask.Delay(1000);
        CRIAudioManager.Instance.SEManager.Play("SE_Click");
    }
}
