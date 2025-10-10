using UnityEngine;

public class PotionPanel : Panel
{
    [SerializeField,Header("‰ñ•œ—Ê")]
    private int _heal;

    public override void Effect()
    {
        _fgs._playerHP += _heal;
        base.Effect();
    }
}
