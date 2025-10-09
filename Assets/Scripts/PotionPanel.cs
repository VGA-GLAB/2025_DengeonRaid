using UnityEngine;

public class PotionPanel : Panel
{
    [SerializeField,Header("‰ñ•œ—Ê")]
    int _heal;

    //Player‚ÌHP‚É‚È‚é•Ï”(‰¼)
    int HP;
    //Panel‚ÌÁ‚¦‚½–‡”‚ğŠÇ—‚·‚é•Ï”(‰¼)
    int _deletePanelNumber;
    public override void Effect()
    {
        HP += _heal * _deletePanelNumber;
        base.Effect();
    }
}
