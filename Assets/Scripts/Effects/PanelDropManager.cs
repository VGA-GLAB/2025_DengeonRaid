using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///         パネル演出の管理クラス
/// </summary>
public class PanelDropManager : MonoBehaviour
{
    public bool IsDropping => _isPanelsDropping;

    [SerializeField] private float _dropDelayInterval = 0.05f;
    private bool _isPanelsDropping;
    private int _droppingPanelCount;

    // DropAllを呼んだ時の情報を保持（完了時に遷移させるため）
    private bool _isSkillUsed;
    private Action _onAllDropped;

    private void Start()
    {
        _isPanelsDropping = false;
        _droppingPanelCount = 0;
    }
    /// <summary>
    ///         取得したリストの演出を再生する
    /// </summary>
    public void DropAll(List<(Panel panel, Vector3 from, Vector3 target)> panels, bool isSkillUsed, Action onComplete = null)
    {
        _isPanelsDropping = true;
        _isSkillUsed = isSkillUsed;
        _onAllDropped = onComplete;
        _droppingPanelCount = panels.Count;

        // 0件なら即完了扱い
        if (_droppingPanelCount == 0)
        {
            AllDropped();
            return;
        }

        foreach (var info in panels)
        {
            Panel dropPanel = info.panel;
            if (dropPanel == null)
            {
                OnePanelDropFinished();
                continue;
            }

            PanelDropEffect dropEffect = dropPanel.GetComponent<PanelDropEffect>();
            if (dropEffect != null)
            {
                float delay = -dropPanel.BoardPos.y * _dropDelayInterval;
                dropEffect.PlayDrop(info.from, info.target, delay, OnePanelDropFinished);
            }
            else
            {
                // Effectが無いなら完了扱いでカウントを減らす
                OnePanelDropFinished();
            }
        }
    }

    /// <summary>
    ///         1パネルの落下演出が完了したときに呼ばれるコールバック。
    ///         すべて終わったらAllDroppedを呼ぶ。
    /// </summary>
    private void OnePanelDropFinished()
    {
        _droppingPanelCount -= 1;

        if (_isPanelsDropping && _droppingPanelCount <= 0)
        {
            AllDropped();
        }
    }

    /// <summary>
    ///          全パネルのDrop演出が完了したタイミングで呼ばれる。
    /// </summary>
    private void AllDropped()
    {
        _isPanelsDropping = false;

        // Drop完了コールバック
        _onAllDropped?.Invoke();
        _onAllDropped = null;

        if (!_isSkillUsed)
            ReferenceManager.Instance.GameDirector.SpawnNewPanelFinished();
        else
            ReferenceManager.Instance.Igsm.ChangeState<SIGIdle>();
    }
}
