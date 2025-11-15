using System.Collections.Generic;
using UnityEngine;

/// <summary>
///         パネル演出の管理クラス
/// </summary>
public class DeletePanelEffectManager : MonoBehaviour
{
    [SerializeField, Header("移動場所")]
    private Transform _targetObj;

    [SerializeField, Header("経由地点")]
    private Transform _viaObj;

    [SerializeField, Header("演出時間差")]
    private float _delay = 0.1f;

    private float _currentDelay = 0f;

    //  のちに追加予定
    //public void ResetDelay()
    //{
    //    _currentDelay = 0f;
    //}

    //public void EffectMove(List<Panel> panels)
    //{
    //    foreach (var panel in panels)
    //    {
    //        _currentDelay += _delay;
    //        DeletePanelEffect deletePanelEffect = panel.GetComponent<DeletePanelEffect>();
    //        if (!deletePanelEffect)
    //            deletePanelEffect.PanelMove(_viaObj, _targetObj, _currentDelay);
    //    }
    //    _currentDelay = 0f;
    //}

    /// <summary>
    ///         パネルを縮小する演出を再生する
    /// </summary>
    /// <param name="panel"></param>
    public void EffectScale(Panel panel)
    {
        DeletePanelEffect deletePanelEffect = panel.GetComponent<DeletePanelEffect>();
        deletePanelEffect.PanelScale();
    }

    /// <summary>
    ///         パネルを元のサイズに戻す演出を再生する
    /// </summary>
    /// <param name="panel"></param>
    public void EffectReturnScale(Panel panel)
    {
        DeletePanelEffect deletePanelEffect = panel.GetComponent<DeletePanelEffect>();
        deletePanelEffect.ReturnPanelScale();
    }
}
