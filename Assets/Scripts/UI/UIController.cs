using DG.Tweening;
using System;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private ReferenceManager _rm;
    private PlayerController _player;

    [Header("ゲージ揺れパラメータ：揺れ時間")]
    [SerializeField] private float _guageShakeDuration = 0.2f;

    [Header("ゲージ揺れパラメータ：強さ")]
    [SerializeField] private float _guageShakeStrength = 20f;

    [Header("ゲージ揺れパラメータ：揺れの頻度")]
    [SerializeField] private int _guageShakeVibrato = 50;

    [Header("ゲージ揺れパラメータ：乱れ具合")]
    [SerializeField, Range(0f, 180f)] private float _guageShakeRandomness = 30f;

    #region  ライフサイクル
    private void Start()
    {
        _rm = ReferenceManager.Instance;
        _player = _rm.PlayerController;

        _rm.Igsm.States[typeof(SIGEliminatePanel)].OnExit += UpdateUI;
        _rm.Igsm.States[typeof(SIGEnemyTurn)].OnExit += UpdateUI;
        _rm.Igsm.States[typeof(SIGShop)].OnExit += UpdateUI;
        UpdateUI();
    }
    #endregion

    #region Publicメソッド

    public void UpdateUI()
    {
        UpdatePlayerHp();
        UpdateShield();
        UpdateMoney();
        UpdateExp();
        UpdateBaseAttack();
        UpdateMissileAttack();
    }

    public void WipeIn(Action callback)
    {
        _rm.UIWipeImage.SetActive(true);
        _rm.UIWipeImage.transform.DOMoveY(0, 1f).OnComplete(callback.Invoke);
    }

    public void PlayHpGuageShake()
    {
        _rm.UIPlayerHpGuage.transform.DOShakePosition(_guageShakeDuration, _guageShakeStrength, _guageShakeVibrato, _guageShakeRandomness, false, false);
    }

    public void PlayShieldGuageShake()
    {
        _rm.UIPlayerShieldGuage.transform.DOShakePosition(_guageShakeDuration, _guageShakeStrength, _guageShakeVibrato, _guageShakeRandomness, false, false);
    }

    public void ShowPanelInfo(Panel panel)
    {
        // パネルのWorld Space Positionを画面上のScreen Positionに変換する
        // ※UICanvasのRender Modeが「Screen Space - Camera」の場合、計算基準はUIが使っているCameraとなるため、
        // 　パラメータのcameraにはUICanvas.worldCameraを渡すこと
        // 　ちなみに「Screen Space - Overlay」の場合は、MainCamera.WorldToViewPortで計算するほうが安定するらしい
        Vector3 panelScreenPos = RectTransformUtility.WorldToScreenPoint(_rm.UICanvas.worldCamera, panel.transform.position);
        // さらに画面上のScreen PositionをUICanvasに対するlocalPositionに変換する
        // 同様に、UICanvas.worldCameraを渡す
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _rm.UICanvas.GetComponent<RectTransform>(),
            panelScreenPos,
            _rm.UICanvas.worldCamera,
            out Vector2 localPos
        );
        _rm.UIPanelInfo.gameObject.SetActive(true);
        _rm.UIPanelInfo.anchoredPosition = localPos;
        _rm.UIPanelInfo.GetComponent<PanelInfoController>().SetEnemyInfoText(panel);
    }

    public void HidePanelInfo()
    {
        _rm.UIPanelInfo.gameObject.SetActive(false);
    }
    #endregion

    #region Privateメソッド
    private void UpdatePlayerHp()
    {
        _rm.UIPlayerHpText.text = $"{_player.Hp:000}";
        _rm.UIPlayerMaxHpText.text = $"{_player.HpMax:000}";

        _rm.UIPlayerHpLossGuage.value = _rm.UIPlayerHpGuage.value;
        _rm.UIPlayerHpGuage.value = (float)_player.Hp / (float)_player.HpMax;
        _rm.UIPlayerHpLossGuage.DOValue(_rm.UIPlayerHpGuage.value, 1f).SetEase(Ease.OutExpo);
    }

    private void UpdateShield()
    {
        _rm.UIPlayerShieldText.text = $"{_player.Shield:000}";
        _rm.UIPlayerMaxShieldText.text = $"{_player.ShieldMax:000}";

        _rm.UIPlayerShieldLossGuage.value = _rm.UIPlayerShieldGuage.value;
        _rm.UIPlayerShieldGuage.value = (float)_player.Shield / (float)_player.ShieldMax;
        _rm.UIPlayerShieldLossGuage.DOValue(_rm.UIPlayerShieldGuage.value, 1f).SetEase(Ease.OutExpo);
    }

    private void UpdateShieldExp()
    {
    }

    private void UpdateExp()
    {
        _rm.ExpGuage.value = (float)_player.Exp / (float)_player.ExpMax;
    }

    private void UpdateMoney()
    {
        _rm.UIMoneyText.text = $"{_player.Money:000}";
        _rm.UIMaxMoneyText.text = $"{_player.MoneyMax:000}";
        _rm.UIMoneyGuage.value = (float)_player.Money / (float)_player.MoneyMax;
    }

    private void UpdateBaseAttack()
    {
        _rm.UIBaseAttackText.text = $"{_player.BaseAttack}";
    }

    private void UpdateMissileAttack()
    {
        _rm.UIMissileAttackText.text = $"{_player.WeaponAttack}";
    }

    #endregion
}