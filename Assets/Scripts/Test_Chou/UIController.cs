using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private ReferenceManager _rm;

    #region  ライフサイクル
    private void Start()
    {
        _rm = ReferenceManager.Instance;
    }
    #endregion

    #region Publicメソッド

    public void UpdateHp(int value)
    {
        // 数字の更新
        _rm.UIPlayerHp.text = value.ToString();
        // TODO ゲージ更新とか
    }
    
    public void UpdateArmor(int value)
    {
    }
    
    public void UpdateArmorExp(int value)
    {
    }
    
    public void UpdateExp(int value)
    {
    }
    
    public void UpdateGold(int value)
    {
    }
    
    #endregion
}