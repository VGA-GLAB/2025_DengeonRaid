using UnityEngine;

public class EnemyPanel : Panel
{
    [SerializeField, Header("‰ñ•œ—Ê")]
    private int _damage;

    public override void Effect()
    {
        Fgs.PlayerHP -= _damage;
        if (Fgs.PlayerHP <= 0)
        {
            Fgs.IsDeath = true;
        }
        else
        {
            Fgs.IsDeath = false;
        }
        base.Effect();
    }
}
