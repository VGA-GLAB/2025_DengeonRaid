using UnityEngine;

public class Panel : MonoBehaviour
{
    public Vector2Int BoardPos { get; set; }

    public virtual string PanelGroupTag => "Default";

    [SerializeField]
    protected Figures Fgs;
    public void Initialize(Vector2Int pos)
    {
        BoardPos = pos;
    }

    public virtual void Effect()
    {

    }
}
