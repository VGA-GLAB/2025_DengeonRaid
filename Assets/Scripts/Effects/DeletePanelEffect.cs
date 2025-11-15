using DG.Tweening;
using UnityEngine;

/// <summary>
///         パネル演出を実行するクラス
/// </summary>
public class DeletePanelEffect : MonoBehaviour
{
    [SerializeField, Header("移動速度")]
    private float _moveSpeed;

    [SerializeField, Header("Panel選択時に縮小するパネルのサイズ")]
    private float _panelMinimalizeSize;

    [SerializeField, Header("Panel選択時に縮小するパネルのサイズ変更速度")]
    private float _panelMinimalizeSpeed;

    [SerializeField,Header("Panelの選択を解除したときにサイズを戻す速度")]
    private float _panelReturnSpeed;

    [SerializeField, Header("移動時に縮小する時のパネルのサイズ")]
    private float _panelMoveMinimalizeSize;

    [SerializeField, Header("移動時に縮小する時のパネルのサイズ変更速度")]
    private float _panelMoveMinimalizeSpeed;

    [SerializeField, Header("移動時に拡大する時のパネルのサイズ")]
    private float _panelExpantionSize;

    [SerializeField, Header("移動時に拡大する時のパネルのサイズ変更速度")]
    private float _panelExpantionSpeed;

    /// <summary>
    ///         消去したパネルをアニメーションで移動させる
    /// </summary>
    public void PanelMove(Transform viaObj,Transform targetObj,float delay)
    {
        transform.DOPath(new Vector3[] {viaObj.position,targetObj.position }, _moveSpeed, PathType.CatmullRom);
        DOTween.Sequence()
            .AppendInterval(delay)
            .Append(transform.DOScale(_panelExpantionSize, _panelExpantionSpeed))
            .Append(transform.DOScale(_panelMoveMinimalizeSize, _panelMoveMinimalizeSpeed));
    }

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
