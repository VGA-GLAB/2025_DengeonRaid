using UnityEngine;

public class EnemyPanel : Panel
{
    [SerializeField, Header("�񕜗�")]
    int _damage;

    //Player��HP�ɂȂ�ϐ�(��)
    int HP;
    //Panel�̏������������Ǘ�����ϐ�(��)
    int _deletePanelNumber;
    public override void Effect()
    {
        HP -= _damage * _deletePanelNumber;
        base.Effect();
    }
}
