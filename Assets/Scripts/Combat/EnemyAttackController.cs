using System.Collections;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    private ReferenceManager _rm;
    private EnemyAttackProcessor _processor;
    private WaitForSeconds _waitOneSecond;

    #region ライフサイクル
    private void Start()
    {
        _rm = ReferenceManager.Instance;
        _rm.Igsm.States[typeof(SIGEnemyTurn)].OnEnter += ProcessEnemyAttack;
        _waitOneSecond = new WaitForSeconds(1f);
    }
    #endregion
    /// <summary>
    /// 敵攻撃処理を実行する
    /// </summary>
    public void ProcessEnemyAttack()
    {
        StartCoroutine(ProcessCoroutine());
    }

    private IEnumerator ProcessCoroutine()
    {
        _processor = new EnemyAttackProcessor(_rm.BoardManager, _rm.PlayerController);
        if (_processor.EnemyExists())
        {
            _processor.Process();
            _rm.UIEnemyAttackPanel.SetActive(true);
            _rm.UIEnemyDamageText.text = "Damage : " + _processor.FinalDamage;
            yield return _waitOneSecond;
            _rm.UIEnemyAttackPanel.SetActive(false);
            _processor.ApplyDamageToPlayer();
        }
        _processor = null;  
        _rm.GameDirector.EnemyAttackFinished();
    }
}
