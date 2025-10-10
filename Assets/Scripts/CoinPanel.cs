using UnityEngine;

public class CoinPanel : Panel
{
    [SerializeField,Header("1つのパネルにつき何コイン手に入れるか")]
    private int _coin;

    //コインの所有数を管理する変数(仮)
    private int _wallet;
    //Panelの消えた枚数を管理する変数(仮)
    private int _deletePanelNumber;
    public override void Effect()
    {
        _wallet += _coin * _deletePanelNumber;
        base.Effect();
    }
}
