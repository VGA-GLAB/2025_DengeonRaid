using UnityEngine;

public class PotionPanel : Panel
{
    [SerializeField,Header("�񕜗�")]
    int _heal;

    //Player��HP�ɂȂ�ϐ�(��)
    int HP;
    //Panel�̏������������Ǘ�����ϐ�(��)
    int _deletePanelNumber;
    public override void Effect()
    {
        HP += _heal * _deletePanelNumber;
        base.Effect();
    }
}
