using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Shop/ItemDatabase")]
/// <summary>
///         アイテム一覧をまとめるSO
/// </summary>
public class ShopItemDatabase : ScriptableObject
{
    public List<ShopItemData> items;
}
