using UnityEngine;
using UnityEngine.UI;

/// <summary>
///         スキルの基底クラス
/// </summary>
public abstract class SkillBase : MonoBehaviour
{
    protected BoardManager _boardManager;

    [Header("スキルEnum")]
    [SerializeField] private SkillEnum _skillEnum = SkillEnum.None;
    [Header("解放フラグ")]
    [SerializeField] private bool _isUnlocked = false;
    [Header("スキル使用回数")]
    [SerializeField] private int _maxSkillUses = 1;
    [Header("使用可能時の色")]
    [SerializeField] private Color _availableColor = Color.yellow;
    [Header("使用不可時の色")]
    [SerializeField] private Color _unavailableColor = Color.gray6;

    [Header("参照")]
    [SerializeField, Tooltip("スキルアイコン")] private GameObject _icon;
    [SerializeField, Tooltip("ロックアイコン")] private GameObject _lock;
    

    private int _remainingSkillUses;

    /// <summary>スキル識別Enum</summary>
    public SkillEnum SkillEnum => _skillEnum;

    private void Start()
    {
        _boardManager = ReferenceManager.Instance.BoardManager;
        _remainingSkillUses = _maxSkillUses;
        _icon.GetComponent<Image>().color = _availableColor;
    }

    /// <summary>
    ///         スキル使用を試みる
    /// </summary>
    public void TryUseSkill()
    {
        // 解放状態の確認
        if (!_isUnlocked)
        {
            Debug.LogWarning("スキルが解放されていません。");
            return;
        }

        // スキル使用回数の確認
        if (_remainingSkillUses <= 0)
        {
            Debug.LogWarning("スキルの使用回数が残っていません。");
            return;
        }

        ActivateSkill();
        _remainingSkillUses--;
        if( _remainingSkillUses <= 0)
        {
            _icon.GetComponent<Image>().color = _unavailableColor;
        }
    }

    /// <summary>
    /// スキルを解放する
    /// </summary>
    public void Unlock()
    {
        _isUnlocked = true;
        if (_lock != null) _lock.SetActive(false);
    }

    /// <summary>
    ///         スキル使用時の処理
    /// </summary>
    public abstract void ActivateSkill();
}
