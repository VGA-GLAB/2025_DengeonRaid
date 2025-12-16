using DG.Tweening;
using UnityEngine;

/// <summary>
///         選択系をしたときに呼び出されるクラス
/// </summary>
public class SelectionScaleEffect : MonoBehaviour
{
    [SerializeField, Header("Panel選択時に縮小するパネルのサイズ")]
    private float _panelMinimalizeSize;

    [SerializeField, Header("Panel選択時に縮小するパネルのサイズ変更速度")]
    private float _panelMinimalizeSpeed;

    [SerializeField, Header("Panelの選択を解除したときにサイズを戻す速度")]
    private float _panelReturnSpeed;

    /// <summary>
    ///         選択されたときにパネルを縮小させる
    /// </summary>
    public void PanelScale()
    {
        transform.DOScale(_panelMinimalizeSize, _panelMinimalizeSpeed);
        Panel judgePanel = this.gameObject.GetComponent<Panel>();

        if (judgePanel is PotionPanel)
            CRIAudioManager.CRISEManager.Play("SE_TouchPotion");
        else if (judgePanel is SwordPanel)
            CRIAudioManager.CRISEManager.Play("SE_TouchRocket");
        else if (judgePanel is ShieldPanel)
            CRIAudioManager.CRISEManager.Play("SE_TouchShield");
        else if (judgePanel is CoinPanel)
            CRIAudioManager.CRISEManager.Play("SE_TouchMoney");
        else if (judgePanel is EnemyPanel)
            CRIAudioManager.CRISEManager.Play("SE_TouchEnemy");
    }

    /// <summary>
    ///         選択が解除されたときにパネルを元のサイズに戻す
    /// </summary>
    public void ReturnPanelScale()
    {
        transform.DOScale(1f, _panelReturnSpeed);
    }
}
