using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
///         パネル演出の管理クラス
/// </summary>
public class PanelDropManager : MonoBehaviour
{
    [SerializeField] private float _dropDelayInterval = 0.05f;
    private bool _isPanelsDropping;
    private int _droppingPanelCount;

    private void Start()
    {
        _isPanelsDropping = false;
        _droppingPanelCount = 0;
    }
    /// <summary>
    ///         取得したリストの演出を再生する
    /// </summary>
    public void DropAll(List<Panel> panels)
    {
        _isPanelsDropping = true;
        _droppingPanelCount = panels.Count;
        foreach (var panel in panels)
        {
            PanelDropEffect dropEffect = panel.GetComponent<PanelDropEffect>();
            if (dropEffect != null)
            {
                float delay = -panel.BoardPos.y * _dropDelayInterval;
                dropEffect.PlayDrop(panel.BoardPos, delay, OnePanelDropFinished);
            }
        }
    }

    private void OnePanelDropFinished()
    {
        _droppingPanelCount -= 1;
        if (_isPanelsDropping && _droppingPanelCount == 0)
        {
            _isPanelsDropping = false;
            ReferenceManager.Instance.GameDirector.SpawnNewPanelFinished();
        }
    }
}
