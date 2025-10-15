using UnityEngine;

public class Panel : MonoBehaviour
{
    public Vector2Int BoardPos { get; set; }
    public int PanelId { get; set; }

    public Figures _fgs;
    public void Initialize(Vector2Int pos, int panel)
    {
        _fgs = FindAnyObjectByType<Figures>();
        BoardPos = pos;
        PanelId = panel;
    }

    public virtual void Effect()
    {

    }
}
