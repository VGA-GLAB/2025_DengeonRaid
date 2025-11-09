using DG.Tweening;
using UnityEngine;

public class DeletePanelEffect : MonoBehaviour
{
    [SerializeField, Header("移動場所")]
    private Vector3 _targetPos;

    [SerializeField, Header("経由地点")]
    private Vector3 _viaPos;

    [SerializeField, Header("移動速度")]
    private float _moveSpeed;

    [SerializeField, Header("縮小時のパネルのサイズ")]
    private Vector3 _panelMinimalizeSize;

    [SerializeField, Header("縮小時のパネルのサイズ変更速度")]
    private float _panelMinimalizeSpeed;

    [SerializeField, Header("拡大時のパネルのサイズ")]
    private float _panelExpantionSpeed;

    [SerializeField, Header("拡大時のパンルのサイズ変更速度")]
    private float _panelExpantionSize;

    public void PanelMove()
    {
        DOTween.Sequence()
            .Join(transform.DOPath(new Vector3[] { _viaPos, _targetPos }, _moveSpeed, PathType.CatmullRom))
            .Join(transform.DOScale(_panelExpantionSize, _panelExpantionSpeed));
    }

    public void PanelScale()
    {
        transform.DOScale(_panelMinimalizeSize, _panelMinimalizeSpeed);
    }
}
