using UnityEngine;

public class Panel : MonoBehaviour
{
    public Vector2Int BoardPos { get; set; }
    public int PanelId { get; set; }

    public void Initialize(Vector2Int pos, int panel)
    {
        BoardPos = pos;
        PanelId = panel;
    }

    public virtual void Effect()
    {

    }
}
