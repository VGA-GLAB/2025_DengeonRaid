using DG.Tweening;
using System;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private ReferenceManager _rm;
    private PlayerController _player;

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
        CRIAudioManager.CRISEManager.Play("SE_ActivationShield");
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