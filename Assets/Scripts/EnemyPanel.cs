using UnityEngine;

public class EnemyPanel : Panel
{
    [SerializeField, Header("‰ñ•œ—Ê")]
    int _damage;

    //Player‚ÌHP‚É‚È‚é•Ï”(‰¼)
    int HP;
    //Panel‚ÌÁ‚¦‚½–‡”‚ğŠÇ—‚·‚é•Ï”(‰¼)
    int _deletePanelNumber;
    public override void Effect()
    {
        HP -= _damage * _deletePanelNumber;
        base.Effect();
    }
}
