using UnityEngine;

public class PotionPanel : Panel
{
    [SerializeField,Header("�񕜗�")]
    private int _heal;

    //Player��HP�ɂȂ�ϐ�(��)
    private int HP;
    //Panel�̏������������Ǘ�����ϐ�(��)
    private int _deletePanelNumber;
    public override void Effect()
    {
        HP += _heal * _deletePanelNumber;
        base.Effect();
    }
}
