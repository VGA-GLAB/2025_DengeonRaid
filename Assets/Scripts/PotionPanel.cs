using UnityEngine;

public class PotionPanel : Panel
{
    [SerializeField,Header("�񕜗�")]
    private int _heal;

    public override void Effect()
    {
        Figures._playerHP += _heal;
        base.Effect();
    }
}
