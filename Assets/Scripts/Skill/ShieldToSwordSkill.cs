using UnityEngine;

/// <summary>
///         シールドパネルを剣パネルに変換するスキル
/// </summary>
public class ShieldToSwordSkill : SkillBase
{
    [SerializeField] private SwordPanel _swordPanelPrefab;

    public override void ActivateSkill()
    {
        Panel[,] panels = _boardManager.GetBoardArray;

        foreach (var panel in panels)
        {
            if (panel is ShieldPanel shieldPanel)
            {
                _boardManager.ReplacePanel(shieldPanel.BoardPos, _swordPanelPrefab, true);
            }
        }
    }
}
