using DG.Tweening;
using UnityEngine;

public class Panel : MonoBehaviour
{
    public Vector2Int BoardPos { get; set; }

    [Header("パネル識別情報")]
    [SerializeField, Tooltip("同じ種類パネルを区別するためのID")]
    private int _panelId;

    [SerializeField,Tooltip("このパネルが属するグループ")]
    private PanelGroup _panelGroup;

    [SerializeField, Header("パネル変更時の目標サイズ")]
    private Vector3 _panelTargetSize;

    [SerializeField, Header("パネル変更時のサイズ変更速度")]
    private float _panelChangeSpeed;

    [SerializeField, Header("パネル変更時のFade速度")]
    private float _panelFadeSpeed;

    private SpriteRenderer _spriteRenderer;
    private Color _defaultColor;

    /// <summary>
    ///         パネルのID(読み取り用)
    /// </summary>
    public int PanelId => _panelId;

    /// <summary>
    ///         このパネルが属するグループ(読み取り用)
    /// </summary>
    public PanelGroup Group => _panelGroup;

    private void Awake()
    {
        if(_spriteRenderer == null)
            _spriteRenderer = GetComponent<SpriteRenderer>();

        _defaultColor = _spriteRenderer.color;
    }

    public void Initialize(Vector2Int pos)
    {
        BoardPos = pos;
    }

    public void SetHighlight(bool isOn)
    {
        _spriteRenderer.color = isOn ? Color.yellow : _defaultColor;
    }

    public virtual void Effect(PreviewPlayerData preview)
    {
        Debug.Log("Effectメソッドが未実装");
    }

    public virtual void DestroyThis()
    {
        Debug.Log("DestroyThisメソッドが未実装");
    }

    public void PanelChange()
    {
        transform.DOScale(_panelTargetSize, _panelChangeSpeed);
        GetComponent<Renderer>().material.DOFade(0, _panelFadeSpeed);
    }
}
