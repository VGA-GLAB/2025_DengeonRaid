using UnityEngine;

public class CoinPanel : Panel
{
    [SerializeField,Header("1�̃p�l���ɂ����R�C����ɓ���邩")]
    private int _coin;

    public override void Effect()
    {
        _fgs._wallet += _coin;
        base.Effect();
    }
}
