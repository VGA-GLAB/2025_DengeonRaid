using UnityEngine;

public class PotionPanel : Panel
{
    [SerializeField,Header("‰ñ•œ—Ê")]
    private int _heal;

    public override void Effect()
    {
        Figures._playerHP += _heal;
        base.Effect();
    }
}
