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

    [SerializeField, Header("パネルのサイズ")]
    private Vector3 _panelSize;

    [SerializeField, Header("パネルのサイズ変更速度")]
    private float _panelScaleSpeed;

    public void PanelMove()
    {
        transform.DOPath(new Vector3[] { _viaPos, _targetPos }, _moveSpeed, PathType.CatmullRom);
    }

    public void PanelScale()
    {
        transform.DOScale(_panelSize, _panelScaleSpeed);
    }
}
