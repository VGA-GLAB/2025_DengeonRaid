using UnityEngine;

public class CoinPanel : Panel
{
    [SerializeField,Header("1�̃p�l���ɂ����R�C����ɓ���邩")]
    int _coin;

    //�R�C���̏��L�����Ǘ�����ϐ�(��)
    int _wallet;
    //Panel�̏������������Ǘ�����ϐ�(��)
    int _deletePanelNumber;
    public override void Effect()
    {
        _wallet += _coin * _deletePanelNumber;
        base.Effect();
    }
}
