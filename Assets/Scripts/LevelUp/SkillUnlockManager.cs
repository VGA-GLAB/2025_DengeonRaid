using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillUnlockManager : MonoBehaviour
{
    private ReferenceManager _rm;
    private List<SkillEnum> _unlockedSkills;
    [SerializeField] List<GameObject> _skills;

    public List<SkillEnum> UnlockedSkills => _unlockedSkills;

    #region ライフサイクル
    private void Start()
    {
        _rm = ReferenceManager.Instance;
        _unlockedSkills = new List<SkillEnum>();
    }
    #endregion

    #region Publicメソッド
    /// <summary>
    /// スキルを解放する
    /// </summary>
    /// <param name="lvupItem"></param>
    public void UnlockSkill(LevelUpItem lvupItem)
    {
        foreach(GameObject go in _skills)
        {
            SkillBase skill = go.GetComponent<SkillBase>();
            if(skill.SkillEnum == lvupItem.SkillToUnlock)
            {
                skill.Unlock();
                _unlockedSkills.Add(lvupItem.SkillToUnlock);
                break;
            }
        }
    }

    /// <summary>
    /// スキルが解放済みか判定する
    /// </summary>
    /// <param name="skill"></param>
    /// <returns>true:解放済み, false：未解放</returns>
    public bool IsSkillUnlocked(LevelUpItem skill)
    {
        return _unlockedSkills.Contains(skill.SkillToUnlock);
    }
    #endregion
}
