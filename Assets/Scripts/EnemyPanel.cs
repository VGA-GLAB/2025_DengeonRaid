using UnityEngine;

public class EnemyPanel : Panel
{
    [SerializeField, Header("�񕜗�")]
    private int _damage;

    //Player��HP�ɂȂ�ϐ�(��)
    private int HP;
    //Panel�̏������������Ǘ�����ϐ�(��)
    private int _deletePanelNumber;
    public override void Effect()
    {
        HP -= _damage * _deletePanelNumber;
        base.Effect();
    }
}
