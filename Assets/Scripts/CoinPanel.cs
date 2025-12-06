using UnityEngine;

public class CoinPanel : Panel
{
    [SerializeField,Header("1つのパネルにつき何コイン手に入れるか")]
    private int _coin;

    public override void Effect(PreviewPlayerData preview)
    {
        preview.Money += _coin;
    }
    public override void DestroyThis()
    {
        DeletePanelEffectManager.Instance.EffectMove(this);

        ReferenceManager.Instance.BoardManager.RemovePanelFromBoard(this);
        Destroy(gameObject);
    }
}
