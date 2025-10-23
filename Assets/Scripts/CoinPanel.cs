using UnityEngine;

public class CoinPanel : Panel
{
    public override string PanelGroupTag => "Coin";

    [SerializeField,Header("1つのパネルにつき何コイン手に入れるか")]
    private int _coin;

    public override void Effect()
    {
        Fgs.Wallet += _coin;
        base.Effect();
    }
}
