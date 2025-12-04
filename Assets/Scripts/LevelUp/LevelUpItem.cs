using System;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelUpItem", menuName = "Scriptable Objects/LevelUpItem")]
public class LevelUpItem : ScriptableObject
{
    public LevelUpEffectType Type;
    [TextArea] public string ItemName;
    public Sprite Icon;
    public int Value;
    public SkillEnum SkillToUnlock;
    [TextArea, Range(0, 2)] public string Description;
}

public enum LevelUpEffectType
{
    MaxHp,
    MaxShield,
    BaseAttack,
    WeaponAttack,
    SkillUnlock
}

public enum SkillEnum
{
    None,
    AttackAll,
    GetHeart,
    ShieldToMissile
}
