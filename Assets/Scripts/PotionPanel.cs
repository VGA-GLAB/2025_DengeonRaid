using UnityEngine;

public class PotionPanel : Panel
{
    [SerializeField,Header("�񕜗�")]
    private int _heal;

    public override void Effect()
    {
        _fgs._playerHP += _heal;
        base.Effect();
    }
}
