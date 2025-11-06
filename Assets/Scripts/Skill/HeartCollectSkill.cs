using UnityEngine;

public class HeartCollectSkill : SkillBase
{
    public override void ActivateSkill()
    {
        Panel[,] panels = _boardManager.GetBoardArray;
        foreach (var panel in panels)
        {
            if (panel is PotionPanel potionPanel)
            {
                _boardManager.DeleatePanel(potionPanel.BoardPos);
            }
        }
    }
}
