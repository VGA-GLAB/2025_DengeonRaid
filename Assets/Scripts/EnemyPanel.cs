using UnityEngine;

public class EnemyPanel : Panel
{
    public override string PanelGroupTag => "Battle";

    [SerializeField, Header("回復量")]
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
