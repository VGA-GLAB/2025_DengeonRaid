using UnityEngine;

public class EnemyPanel : Panel
{
    [SerializeField, Header("‰ñ•œ—Ê")]
    private int _damage;

    public override void Effect()
    {
        Fgs.PlayerHP -= _damage;
        base.Effect();
    }
}
