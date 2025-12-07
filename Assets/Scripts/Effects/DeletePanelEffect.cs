using DG.Tweening;
using UnityEngine;

/// <summary>
///         パネル演出を実行するクラス
/// </summary>
public class DeletePanelEffect : MonoBehaviour
{
    [SerializeField, Header("移動速度")]
    private float _moveSpeed;

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
    public void PanelMove(Transform viaObj, Transform targetObj)
    {
        ReferenceManager rm = ReferenceManager.Instance;
        GameDirector director = rm.GameDirector;
        transform.DOPath(new Vector3[] { viaObj.position, targetObj.position }, _moveSpeed, PathType.CatmullRom);
        DOTween.Sequence()
            .Append(transform.DOScale(_panelExpantionSize, _panelExpantionSpeed))
            .Append(transform.DOScale(_panelMoveMinimalizeSize, _panelMoveMinimalizeSpeed)
            .OnComplete(() =>
            {
                director.PanelResolvingFinished();
                Destroy(gameObject);
            }));
    }
}
