using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
///         パネル演出の管理クラス
/// </summary>
public class PanelDropManager : MonoBehaviour
{
    [SerializeField] private float _dropDelayInterval = 0.05f;

    /// <summary>
    ///         取得したリストの演出を再生する
    /// </summary>
    public void DropAll(List<Panel> panels)
    {
        foreach (var panel in panels)
        {
            PanelDropEffect dropEffect = panel.GetComponent<PanelDropEffect>();
            if (dropEffect != null)
            {
                float delay = -panel.BoardPos.y * _dropDelayInterval;
                dropEffect.PlayDrop(panel.BoardPos, delay);
            }
        }
    }
}
