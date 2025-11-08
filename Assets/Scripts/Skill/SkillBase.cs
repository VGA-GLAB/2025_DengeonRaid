using UnityEngine;

/// <summary>
///         スキルの基底クラス
/// </summary>
public abstract class SkillBase : MonoBehaviour
{
    [SerializeField] protected BoardManager _boardManager;

    /// <summary>
    ///         スキル使用時の処理
    /// </summary>
    public abstract void ActivateSkill();
}
