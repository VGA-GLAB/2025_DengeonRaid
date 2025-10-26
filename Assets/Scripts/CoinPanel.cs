using UnityEngine;

public class CoinPanel : Panel
{
    [SerializeField,Header("1つのパネルにつき何コイン手に入れるか")]
    private int _coin;

 

    public override void Effect(PreviewPlayerData preview)
    {
        preview.Gold += _coin;
    }

    public override void DestroyThis()
    {
        //ReferenceManager.Instance.ChouBoardManager.RemovePanelFromBoard(this);
        Destroy(gameObject);
    }
}
