using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///         1アイテム分のUI表示
/// </summary>
public class LevelUpUI : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _descText;
    [SerializeField] private Button _button;

    private LevelUpItem _lvupItemData;
    private LevelUpUIManager _levelupUIManager;

    /// <summary>
    ///         UIのテキストに値を設定
    /// </summary>
    /// <param name="itemData"></param>
    /// <param name="manager"></param>
    public void Setup(LevelUpItem itemData, LevelUpUIManager manager)
    {
        _lvupItemData = itemData;
        _levelupUIManager = manager;

        _icon.sprite = _lvupItemData.Icon;
        _nameText.text = _lvupItemData.ItemName;
        _descText.text = $"{_lvupItemData.Description}";
        if(itemData.Type != LevelUpEffectType.SkillUnlock)
        {
            _descText.text += $"{ _lvupItemData.Value}";
        }
        else
        {
            _icon.color = Color.darkCyan;
        }
        _button.onClick.AddListener(Selected);
    }

    /// <summary>
    ///         ボタンを使ったイベント
    /// </summary>
    private void Selected()
    {
        _levelupUIManager.ChooseItem(_lvupItemData);
        _levelupUIManager.CloseLevelUp();
    }
}
