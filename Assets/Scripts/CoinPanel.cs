using UnityEngine;

public class CoinPanel : Panel
{
    [SerializeField,Header("1つのパネルにつき何コイン手に入れるか")]
    private int _coin;

    public override void Effect()
    {
        Figures._wallet += _coin;
        base.Effect();
    }
}
