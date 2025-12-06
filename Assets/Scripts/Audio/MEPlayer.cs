using CriWare;
using UnityEngine;

/// <summary>
///         ME再生クラス
/// </summary>
public class MEPlayer : MonoBehaviour
{
    [SerializeField] private string meCueName;

    private void Start()
    {
        CRIAudioManager.CRIBGMManager.Stop();
        CRIAudioManager.CRISEManager.DisposeAll();

        CriAtomSource player = this.gameObject.GetComponent<CriAtomSource>();

        if (player == null) return;
        float v = CRIAudioManager.SoundSettings.LoadBGMVolume();
        player.volume = v;
        player.Play(meCueName);
    }
}
