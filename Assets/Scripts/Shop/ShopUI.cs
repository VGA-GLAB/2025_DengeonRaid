using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///         1アイテム分のUI表示
/// </summary>
public class ShopUI : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _descText;
    [SerializeField] private Button _buyButton;

    private ShopItemData _shopItemData;
    private ShopUIManager _shopUIManager;

    /// <summary>
    ///         UIのテキストに値を設定
    /// </summary>
    /// <param name="shopItemData"></param>
    /// <param name="manager"></param>
    public void Setup(ShopItemData shopItemData, ShopUIManager manager)
    {
        _shopItemData = shopItemData;
        _shopUIManager = manager;

        _icon.sprite = _shopItemData.Icon;
        _nameText.text = _shopItemData.ItemName;
        _descText.text = $"{_shopItemData.Description} {_shopItemData.Value}";

        _buyButton.onClick.AddListener(HandleBuy);
    }

    /// <summary>
    ///         ボタンを使ったイベント
    /// </summary>
    private void HandleBuy()
    {
        _shopUIManager.BuyItem(_shopItemData);
        _shopUIManager.CloseShop();
    }
}
