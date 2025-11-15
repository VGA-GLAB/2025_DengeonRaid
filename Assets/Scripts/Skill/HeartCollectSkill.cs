/// <summary>
///         盤面のハートパネルを全て回収して使用スキル
/// </summary>
public class HeartCollectSkill : SkillBase
{
    public override void ActivateSkill()
    {
        foreach (Panel panel in _boardManager.GetBoardArray)
        {
            if (panel is PotionPanel potionPanel)
            {
                _boardManager.DeleatePanel(panel);
            }
        }
        // 1回だけパネル落下を実行
        _boardManager.ForceDropPanel();
    }
}
