using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HealthDecreaseExpression : MonoBehaviour
{
    [SerializeField]
    private PreviewPlayerData _ppd;
    [SerializeField]
    private EnemyAttackProcessor _eap;
    [SerializeField]
    private Slider _hpGauge;

    private float _beforeHp;

    /// <summary>
    /// 減少前のHPを取得(パネル消す前に実行してもらうつもりで書いたやつ)
    /// </summary>
    public void GetBeforeHP()
    {
        _beforeHp = _ppd.Hp;
    }

    public void DecreaseGauge()
    {
        _beforeHp = _hpGauge.value;

        _hpGauge.DOValue(_beforeHp - _eap.FinalDamage, 1f);
    }
}