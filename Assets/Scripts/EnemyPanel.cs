using UnityEngine;

public class EnemyPanel : Panel
{
    [SerializeField, Header("‰ñ•œ—Ê")]
    private int _damage;

    //Player‚ÌHP‚É‚È‚é•Ï”(‰¼)
    private int HP;
    //Panel‚ÌÁ‚¦‚½–‡”‚ğŠÇ—‚·‚é•Ï”(‰¼)
    private int _deletePanelNumber;
    public override void Effect()
    {
        HP -= _damage * _deletePanelNumber;
        base.Effect();
    }
}
