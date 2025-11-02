using TMPro;
using UnityEngine;

/// <summary>
///         PlayerのステータスUI表示
/// </summary>
public class ShopPlayerStatusUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _hpText;
    [SerializeField] private TMP_Text _attackText;
    [SerializeField] private TMP_Text _shieldText;

    private PreviewPlayerData _previewPlayerData;

    /// <summary>
    ///         初期化
    /// </summary>
    /// <param name="playerData"></param>
    public void SetUp(PreviewPlayerData playerData)
    {
        _previewPlayerData = playerData;
        UpdateUI();
    }

    /// <summary>
    ///         UIの更新
    /// </summary>
    private void UpdateUI()
    {
        _hpText.text = $"HP: {_previewPlayerData.Hp} / {_previewPlayerData.HpMax}";
        _attackText.text = $"Attack: {_previewPlayerData.BaseAttack}";
        _shieldText.text = $"Shield: {_previewPlayerData.Shield} / {_previewPlayerData.ShieldMax}";
    }
}
