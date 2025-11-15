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
    public void DropAll(List<(Panel panel, Vector3 from, Vector3 target)> panels, bool isSkillUsed)
    {
        _isPanelsDropping = true;
        _droppingPanelCount = panels.Count;
        foreach (var info in panels)
        {
            Panel dropPanel = info.panel;
            PanelDropEffect dropEffect = dropPanel.GetComponent<PanelDropEffect>();
            if (dropEffect != null)
            {
                float delay = -dropPanel.BoardPos.y * _dropDelayInterval;
                dropEffect.PlayDrop(info.from, info.target, delay, OnePanelDropFinished);
            }
        }

        if (!isSkillUsed)
            ReferenceManager.Instance.GameDirector.SpawnNewPanelFinished();
        else
            ReferenceManager.Instance.Igsm.ChangeState<SIGIdle>();
    }

    private void OnePanelDropFinished()
    {
        _droppingPanelCount -= 1;
        if (_isPanelsDropping && _droppingPanelCount == 0)
        {
            _isPanelsDropping = false;
        }
    }
}
