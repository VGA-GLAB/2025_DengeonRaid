using UnityEngine;
using Cysharp.Threading.Tasks;

public class TestAudioPlayer : MonoBehaviour
{
    private async void Start()
    {
        // AudioManagerの読み込み待ち
        await CRIAudioManager.ReadyTask();

        Debug.Log("AudioManager Ready!");

        // BGM 再生
        CRIAudioManager.CRIBGMManager.Play("ME_Gameover");

        // 1秒後に SE 再生
        await UniTask.Delay(1000);
        CRIAudioManager.CRISEManager.Play("SE_Click");
    }
}
