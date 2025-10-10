using UnityEngine;

public class PotionPanel : Panel
{
    [SerializeField,Header("‰ñ•œ—Ê")]
    private int _heal;

    //Player‚ÌHP‚É‚È‚é•Ï”(‰¼)
    private int HP;
    //Panel‚ÌÁ‚¦‚½–‡”‚ğŠÇ—‚·‚é•Ï”(‰¼)
    private int _deletePanelNumber;
    public override void Effect()
    {
        HP += _heal * _deletePanelNumber;
        base.Effect();
    }
}
