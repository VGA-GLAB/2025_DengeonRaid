using DG.Tweening;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField, Tooltip("Warning SEのフェードアウト時間（ミリ秒）")]
    private int _warningFadeTime;
    [SerializeField, Tooltip("ボスが着地するまでの時間（秒）")]
    private float _bossLandTime;
    [SerializeField, Tooltip("ボス着地時カメラ揺れ時間")]
    private float _cameraShakeDur;
    [SerializeField, Tooltip("ボス着地時カメラ揺れ強度")]
    private float _cameraShakeStrength;
    [SerializeField, Tooltip("ボス着地時カメラ揺れ幅")]
    private int _cameraShakeVibrato;
    [SerializeField, Tooltip("ボス着地時カメラ揺れの乱れ具合")]
    private float _cameraShakeRandomness;
    [SerializeField, Tooltip("ボスがパネルに移動する時の移動時間")]
    private float _moveToPanelDur;

    private ReferenceManager _rm;
    private SEPlayer _warningSEPlayer;

    private void Start()
    {
        _rm = ReferenceManager.Instance;
    }
    
    public void StopBGM()
    {
        CRIAudioManager.CRIBGMManager.Stop();
    }

    public void PlayWarningSE()
    {
        _warningSEPlayer = CRIAudioManager.CRISEManager.PlayFadable("SE_Warning", _warningFadeTime);
    }

    public void FadeOutWarningSE()
    {
        if (_warningSEPlayer is null) return;

        _warningSEPlayer.Stop();
    }

    public void PlayBossDropAnimation()
    {
        StartCoroutine(BossDropAnimationSequence());
    }

    private IEnumerator BossDropAnimationSequence()
    {
        _rm.CutsceneBoss.SetActive(true);
        SEPlayer quakePlayer = CRIAudioManager.CRISEManager.PlayAndGetPlayer("SE_Quake");

        // Boss落下
        yield return _rm.CutsceneBoss.transform.DOMove(Vector3.zero, _bossLandTime).SetEase(Ease.InCubic).WaitForCompletion();

        // Boss着地＆カメラ揺れ
        quakePlayer.Stop();
        CRIAudioManager.CRISEManager.Play("SE_EnemyLanding");
        yield return _rm.MainCamera.DOShakePosition(_cameraShakeDur, _cameraShakeStrength, _cameraShakeVibrato, _cameraShakeRandomness, true).WaitForCompletion();

        // Bossがパネルに移動
        Vector3 bossPanelPos = _rm.BossPanel.transform.position;
        yield return DOTween.Sequence()
            .Append(_rm.CutsceneBoss.transform.DOMove(bossPanelPos, _moveToPanelDur))
            .Join(_rm.CutsceneBoss.transform.DOScale(Vector3.one, _moveToPanelDur))
            .Play().WaitForCompletion();

        // 演出完了
        _rm.CutsceneBoss.SetActive(false);
        _rm.BossPanel.ShowBossSprite();
        _rm.GameDirector.BossDropAnimationOver();
    }
}
