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

    public static CRIBGMManager CRIBGMManager => Instance._bgmManager;

    public static CRISEManager CRISEManager => Instance._seManager;

    public static SoundSettingDataSO SoundSettings => Instance._soundSettings;

    public bool IsReady => _isReady;

    [SerializeField] private SoundSettingDataSO _soundSettings;
    private CRIBGMManager _bgmManager;
    private CRISEManager _seManager;
    private Dictionary<string, CriAtomExAcb> _acbDic = new();
    private bool _isReady = false;

    public static UniTask ReadyTask() => UniTask.WaitUntil(() => Instance.IsReady);

    /// <summary>
    ///         cuesheetのロードをする
    /// </summary>
    /// <returns></returns>
    public static async UniTask<Dictionary<string, CriAtomExAcb>> LoadcueSheets(CriAtom criAtom)
    {

        // cueSheetの読み込み完了待ち
        await UniTask.WaitUntil(() => criAtom.cueSheets.All(cs => cs.IsLoading == false));

        Dictionary<string, CriAtomExAcb> dic = new();

        foreach (var cueSheet in criAtom.cueSheets)
        {
            dic[cueSheet.name] = cueSheet.acb;
            Debug.Log($"CueSheet Loaded: {cueSheet.name}");
        }

        return dic;
    }

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

        CriAtom criAtom = FindAnyObjectByType<CriAtom>();
        if (criAtom == null)
        {
            Debug.LogError("CriAtom がシーンに存在しません！");
            return;
        }

        _acbDic = await LoadcueSheets(criAtom);


        // 初期化
        _seManager = new CRISEManager(_acbDic["SE"]);
        _bgmManager = new CRIBGMManager(_acbDic["BGM"]);

        _seManager.SetVolume(_soundSettings.LoadSEVolume());
        _bgmManager.SetVolume(_soundSettings.LoadBGMVolume());

        _isReady = true;
    }
}
