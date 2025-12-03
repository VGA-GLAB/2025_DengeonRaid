using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
#if UNITY_EDITOR
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.Rendering.DebugUI;
#endif
/// <summary>
///         UI生成＆購入処理
/// </summary>
public class LevelUpUIManager : MonoBehaviour
{
    [SerializeField] private LvupSkills _skillItems;
    [SerializeField] private LvupAttrItems _attrItems;
    [SerializeField] private Transform _itemTransform;
    [SerializeField] private GameObject _itemPrefab;
    [SerializeField] private int _itemCountMax;
    [SerializeField] private ShopPlayerStatusUI _playerStatusUI;
    private ReferenceManager _rm;
    private PlayerController _player;
    private SkillUnlockManager _skillUnlockManager;

    private PreviewPlayerData _previewPlayerData;
    //private bool _isInit = false;

    #region ライフサイクル
    private void Start()
    {
        _rm = ReferenceManager.Instance;
        _skillUnlockManager = _rm.SkillUnlockManager;
        _player = _rm.PlayerController;
        this.gameObject.SetActive(false);
    }
    #endregion

    #region Publicメソッド
    public void ChooseItem(LevelUpItem item)
    {
        PreviewPlayerData preview = new PreviewPlayerData(_player);
        switch (item.Type)
        {
            case LevelUpEffectType.MaxHp:
                preview.HpMax += item.Value;
                preview.Hp += item.Value;
                _player.SetResolvePreview(preview);
                _player.ApplyPreview();
                break;
            case LevelUpEffectType.MaxShield:
                preview.ShieldMax += item.Value;
                preview.Shield += item.Value;
                _player.SetResolvePreview(preview);
                _player.ApplyPreview();
                break;
            case LevelUpEffectType.BaseAttack:
                preview.BaseAttack += item.Value;
                _player.SetResolvePreview(preview);
                _player.ApplyPreview();
                break;
            case LevelUpEffectType.WeaponAttack:
                preview.WeaponAttack += item.Value;
                _player.SetResolvePreview(preview);
                _player.ApplyPreview();
                break;
            case LevelUpEffectType.SkillUnlock:
                if (item.SkillPrefab != null)
                {
                    _rm.SkillUnlockManager.UnlockSkill(item);
                }
                break;
            default:
                Debug.LogWarning("Unknown LevelUpEffectType: " + item.Type);
                break;
        }
    }

    /// <summary>
    ///         レベルアップ画面を開く
    /// </summary>
    public void OpenLevelUp()
    {
        gameObject.SetActive(true);
        foreach(RectTransform item in _itemTransform.GetComponentInChildren<RectTransform>())
        {
            Destroy(item.gameObject);
        }
        // 表示するアイテムを取得
        List<LevelUpItem> items = new List<LevelUpItem>();
        foreach (LevelUpItem skill in _skillItems.SkillItems)
        {
            if (_skillUnlockManager.IsSkillUnlocked(skill)) continue;
            
            items.Add(skill);
        }

        int itemCountAvailable = _itemCountMax - items.Count;
        if (itemCountAvailable > 0)
        {
            items.AddRange(GetAttrItems(itemCountAvailable));
        }

        foreach (LevelUpItem item in items)
        {
            GameObject ui = Instantiate(_itemPrefab, _itemTransform);
            LevelUpUI itemUI = ui.GetComponent<LevelUpUI>();
            itemUI.Setup(item, this);
        }
        _playerStatusUI.SetUp(new PreviewPlayerData(_player));
    }

    /// <summary>
    ///         レベルアップ画面を閉じる
    /// </summary>
    public void CloseLevelUp()
    {
        gameObject.SetActive(false);
        _rm.GameDirector.LevelUpFinished();
    }
    #endregion

    #region Privateメソッド
    private List<LevelUpItem> GetAttrItems(int count)
    {
        List<LevelUpItem> rtn = new List<LevelUpItem>();
        List<LevelUpItem> items = _attrItems.AttrItems.ToList();
        while(count > 0)
        {
            int index = Random.Range(0, items.Count);
            rtn.Add(items[index]);
            items.RemoveAt(index);
            count--;
        }
        return rtn;
    }
    #endregion
}
