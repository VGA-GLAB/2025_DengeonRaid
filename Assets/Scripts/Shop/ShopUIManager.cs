using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

/// <summary>
///         UI生成＆購入処理
/// </summary>
public class ShopUIManager : MonoBehaviour
{
    [SerializeField] private ShopItemDatabase _shopItemDatabase;
    [SerializeField] private Transform _shopItemTransform;
    [SerializeField] private GameObject _shopItemPrefab;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private ShopPlayerStatusUI _shopPlayerStutsUI;

    private PreviewPlayerData _previewPlayerData;
    private bool _isInit = false;

    private void Start()
    {
        this.gameObject.SetActive(false);
        InitShop();
    }

    public void BuyItem(ShopItemData item)
    {
        _previewPlayerData = new PreviewPlayerData(_playerController);
        _previewPlayerData.Gold = 0;

         //  対象ステータスを直接操作
        switch (item.ShopEffectType)
        {
            case ShopEffectType.MaxHp:
                _previewPlayerData.HpMax += item.Value;
                _previewPlayerData.Hp = Mathf.Min(_previewPlayerData.Hp + item.Value, _previewPlayerData.HpMax);
                break;
            case ShopEffectType.Attack:
                _previewPlayerData.BaseAttack += item.Value;
                break;
            case ShopEffectType.MaxShild:
                _previewPlayerData.ShieldMax += item.Value;
                _previewPlayerData.Shield = Mathf.Min(_previewPlayerData.Shield + item.Value, _previewPlayerData.ShieldMax);
                break;
            case ShopEffectType.Gold:
                _previewPlayerData.Gold += item.Value;
                break;
        }
    }

    /// <summary>
    ///         ショップを開く
    /// </summary>
    public void OpenShop()
    {
        _shopPlayerStutsUI.SetUp(new PreviewPlayerData(_playerController));
        InitShop();
        gameObject.SetActive(true);
    }

    /// <summary>
    ///         ショップを閉じる
    /// </summary>
    public void CloseShop()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    ///        初期化と生成
    /// </summary>
    private void InitShop()
    {
        //  二重生成防止
        if (_isInit) return;
        _isInit = true;

        foreach (ShopItemData item in _shopItemDatabase.items)
        {
            GameObject ui = Instantiate(_shopItemPrefab, _shopItemTransform);
            ShopUI itemUI = ui.GetComponent<ShopUI>();
            itemUI.Setup(item, this);
        }
    }
}
