using UnityEngine;

public class CoinPanel : Panel
{
    [SerializeField,Header("1�̃p�l���ɂ����R�C����ɓ���邩")]
    private int _coin;

    //�R�C���̏��L�����Ǘ�����ϐ�(��)
    private int _wallet;
    //Panel�̏������������Ǘ�����ϐ�(��)
    private int _deletePanelNumber;
    public override void Effect()
    {
        _wallet += _coin * _deletePanelNumber;
        base.Effect();
    }
}
