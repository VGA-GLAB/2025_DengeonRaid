using CriWare;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
///         Audioの統括クラス
/// </summary>
public class CRIAudioManager : MonoBehaviour
{
    public static CRIAudioManager Instance { get; private set; }

    public CRIBGMManager BGMManager { get; private set; }
    public CRISEManager SEManager { get; private set; }

    private Dictionary<string, CriAtomExAcb> _acbDic = new();
    private bool _isReady = false;

    public bool IsReady => _isReady;

    private async void Awake()
    {
        // インスタンス生成
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        BGMManager = new CRIBGMManager();
        SEManager = new CRISEManager();

        await LoadcueSheets();
    }

    /// <summary>
    ///         cuesheetのロードをする
    /// </summary>
    /// <returns></returns>
    private async UniTask LoadcueSheets()
    {
        CriAtom criAtom = FindAnyObjectByType<CriAtom>();
        if (criAtom == null)
        {
            Debug.LogError("CriAtom がシーンに存在しません！");
            return;
        }

        // cueSheetの読み込み完了待ち
        await UniTask.WaitUntil(() => criAtom.cueSheets.All(cs => cs.IsLoading == false));

        foreach (var sheet in criAtom.cueSheets)
        {
            _acbDic[sheet.name] = sheet.acb;
        }

        _isReady = true;

        // 初期化
        //SEManager.Initialize(_acbDic["SE"]);
        //BGMManager.Initialize(_acbDic["BGM"]);

        BGMManager.SetVolume(SoundSettings.BGMVolume);
        SEManager.SetVolume(SoundSettings.SEVolume);
    }
}
