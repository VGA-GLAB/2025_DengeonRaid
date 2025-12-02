using DG.Tweening;
using UnityEngine;

public class ChangePanelEffect : MonoBehaviour
{
    [SerializeField, Header("パネル変更時の目標サイズ")]
    private Vector3 _panelTargetSize;

    [SerializeField, Header("パネル変更時のサイズ変更速度")]
    private float _panelChangeSpeed;

    [SerializeField, Header("パネル変更時のFade速度")]
    private float _panelFadeSpeed;

    public void PanelChange()
    {
        transform.DOScale(_panelTargetSize, _panelChangeSpeed);
        GetComponent<Renderer>().material.DOFade(0, _panelFadeSpeed);
    }
}
