using UnityEngine;

public class EnemyPanel : Panel
{
    [SerializeField, Header("�񕜗�")]
    private int _damage;

    public override void Effect()
    {
        _fgs._playerHP -= _damage;
        base.Effect();
    }
}
