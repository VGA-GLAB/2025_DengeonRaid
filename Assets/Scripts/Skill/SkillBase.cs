using UnityEngine;

/// <summary>
///         スキルの基底クラス
/// </summary>
public abstract class SkillBase : MonoBehaviour
{
    protected BoardManager _boardManager;

    [Header("スキル使用回数")]
    [SerializeField] private int _maxSkillUses = 1;
    private int _remainingSkillUses;

    private void Start()
    {
        _boardManager = ReferenceManager.Instance.BoardManager;
        _remainingSkillUses = _maxSkillUses;
    }

    /// <summary>
    ///         スキル使用を試みる
    /// </summary>
    public void TryUseSkill()
    {
        // スキル使用回数の確認
        if (_remainingSkillUses <= 0)
        {
            Debug.LogWarning("スキルの使用回数が残っていません。");
            return;
        }

        ActivateSkill();
        _remainingSkillUses--;
    }

    /// <summary>
    ///         スキル使用時の処理
    /// </summary>
    public abstract void ActivateSkill();
}
