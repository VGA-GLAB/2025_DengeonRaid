using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillUnlockManager : MonoBehaviour
{
    private ReferenceManager _rm;
    private List<LevelUpItem> _unlockedSkills;

    public List<LevelUpItem> UnlockedSkills => _unlockedSkills;

    #region ライフサイクル
    private void Start()
    {
        _rm = ReferenceManager.Instance;
        _unlockedSkills = new List<LevelUpItem>();
    }
    #endregion

    #region Publicメソッド
    /// <summary>
    /// スキルを開放する
    /// </summary>
    /// <param name="skill"></param>
    public void UnlockSkill(LevelUpItem skill)
    {
        GameObject skillInstance = Instantiate(skill.SkillPrefab, _rm.SkillPanel.transform);
        _unlockedSkills.Add(skill);
    }

    /// <summary>
    /// スキルが開放済みか判定する
    /// </summary>
    /// <param name="skill"></param>
    /// <returns>true:開放済み, false：未開放</returns>
    public bool IsSkillUnlocked(LevelUpItem skill)
    {
        return _unlockedSkills.Contains(skill);
    }
    #endregion
}
