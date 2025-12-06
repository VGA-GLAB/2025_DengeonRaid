using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthDecreaseExpression : MonoBehaviour
{
    [SerializeField]
    private PreviewPlayerData _ppd;
    [SerializeField]
    private Slider _hpGauge;

    private int _beforeHp;

    /// <summary>
    /// 減少前のHPを取得(パネル消す前に実行してもらうつもりで書いたやつ)
    /// </summary>
    public void GetBeforeHP()
    {
        _beforeHp = _ppd.Hp;
        _hpGauge.value = _beforeHp;
    }

    /// <summary>
    /// 減少した分のHPを減らす処理
    /// </summary>
    public IEnumerator DecreaseGauge()
    {
        while (_beforeHp > _ppd.Hp)
        {
            _beforeHp--;
            _hpGauge.value = _beforeHp;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
