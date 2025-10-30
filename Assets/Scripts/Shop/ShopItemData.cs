using UnityEngine;

[CreateAssetMenu(menuName = "Shop/Item")]
public class ShopItemData : ScriptableObject
{
    public ShopEffectType ShopEffectType;
    public string ItemName;
    public Sprite Icon;
    public int Value;
    [TextArea,Range(0,2)]public string Description;
}

public enum ShopEffectType
{
    MaxHp,
    MaxShild,
    Attack,
    Gold
}
