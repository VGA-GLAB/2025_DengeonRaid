using UnityEngine;

public class EnemyPanel : Panel
{
    [SerializeField, Header("‰ñ•œ—Ê")]
    private int _damage;

    public override void Effect()
    {
        Figures._playerHP -= _damage;
        base.Effect();
    }
}
