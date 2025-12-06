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
    }

    /// <summary>
    ///         選択が解除されたときにパネルを元のサイズに戻す
    /// </summary>
    public void ReturnPanelScale()
    {
        transform.DOScale(1f, _panelReturnSpeed);
    }
}
