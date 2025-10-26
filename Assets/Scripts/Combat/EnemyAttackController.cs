using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    private ReferenceManager _rm;
    private EnemyAttackProcessor _processor;

    #region ライフサイクル
    private void Start()
    {
        _rm = ReferenceManager.Instance;
        _rm.Igsm.States[typeof(SIGEnemyTurn)].OnEnter += ProcessEnemyAttack;
    }
    #endregion
    public void ProcessEnemyAttack()
    {
        _processor = new EnemyAttackProcessor(_rm.ChouBoardManager, _rm.PlayerController);
    }
}
