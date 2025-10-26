using UnityEngine;

public class SwordPanel : Panel
{
    [SerializeField, Header("Playerが与えるダメージに対するバフ")]
    private int _damageBuff;
    public override void Effect(PreviewPlayerData preview)
    {
        preview.PanelAttack += _damageBuff;
    }
    public override void DestroyThis()
    {
        //ReferenceManager.Instance.ChouBoardManager.RemovePanelFromBoard(this);
        Destroy(gameObject);
    }
}
