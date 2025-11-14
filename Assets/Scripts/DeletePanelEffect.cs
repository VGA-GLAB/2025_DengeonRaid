using DG.Tweening;
using UnityEngine;

public class DeletePanelEffect : MonoBehaviour
{
    [SerializeField, Header("移動場所")]
    private Transform _targetObj;

    [SerializeField, Header("経由地点")]
    private Transform _viaObj;

    [SerializeField, Header("移動速度")]
    private float _moveSpeed;

    [SerializeField, Header("Panel選択時に縮小するパネルのサイズ")]
    private float _panelMinimalizeSize;

    [SerializeField, Header("Panel選択時に縮小するパネルのサイズ変更速度")]
    private float _panelMinimalizeSpeed;

    [SerializeField, Header("移動時に縮小する時のパネルのサイズ")]
    private float _panelMoveMinimalizeSize;

    [SerializeField, Header("移動時に縮小する時のパネルのサイズ変更速度")]
    private float _panelMoveMinimalizeSpeed;

    [SerializeField, Header("移動時に拡大する時のパネルのサイズ")]
    private float _panelExpantionSize;

    [SerializeField, Header("移動時に拡大する時のパネルのサイズ変更速度")]
    private float _panelExpantionSpeed;

    public void PanelMove()
    {
        transform.DOPath(new Vector3[] { _viaObj.position, _targetObj.position }, _moveSpeed, PathType.CatmullRom);
        DOTween.Sequence()
            .Append(transform.DOScale(_panelExpantionSize, _panelExpantionSpeed))
            .Append(transform.DOScale(_panelMoveMinimalizeSize, _panelMoveMinimalizeSpeed));
    }

    public void PanelScale()
    {
        transform.DOScale(_panelMinimalizeSize, _panelMinimalizeSpeed);
    }
}
