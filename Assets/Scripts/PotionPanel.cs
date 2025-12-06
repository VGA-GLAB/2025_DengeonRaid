using UnityEngine;

public class PotionPanel : Panel
{
    [SerializeField,Header("回復量")]
    private int _heal;

    public override void Effect(PreviewPlayerData preview)
    {
        preview.Hp += _heal;
    }
    public override void DestroyThis()
    {
        DeletePanelEffectManager.Instance.EffectMove(this);

        ReferenceManager.Instance.BoardManager.RemovePanelFromBoard(this);
        Destroy(gameObject);
    }
}
