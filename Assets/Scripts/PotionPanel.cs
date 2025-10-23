using UnityEngine;

public class PotionPanel : Panel
{
    public override string PanelGroupTag => "Portion";

    [SerializeField,Header("回復量")]
    private int _heal;

    public override void Effect()
    {
        Fgs.PlayerHP += _heal;
        base.Effect();
    }
}
