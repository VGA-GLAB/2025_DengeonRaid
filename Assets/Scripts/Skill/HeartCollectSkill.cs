/// <summary>
///         盤面のハートパネルを全て回収して使用スキル
/// </summary>
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
