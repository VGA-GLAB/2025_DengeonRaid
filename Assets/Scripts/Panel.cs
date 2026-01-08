using UnityEngine;

public class Panel : MonoBehaviour
{
    public Vector2Int BoardPos { get; set; }

    [Header("パネル識別情報")]
    [SerializeField, Tooltip("同じ種類パネルを区別するためのID")]
    private int _panelId;

    [SerializeField, Tooltip("このパネルが属するグループ")]
    private PanelGroup _panelGroup;
    [SerializeField, Header("パネル名")]
    private string _panelName;
    [SerializeField, Header("パネル説明文")]
    private string _panelDescription;

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
        if (_spriteRenderer == null)
            _spriteRenderer = GetComponent<SpriteRenderer>();

        _defaultColor = _spriteRenderer.color;
        CustomAwake();
    }

    /// <summary>
    ///         初期化して位置を設定する
    /// </summary>
    /// <param name="pos">座標</param>
    public void Initialize(Vector2Int pos)
    {
        BoardPos = pos;
    }

    /// <summary>
    ///         パネルを暗くする
    /// </summary>
    /// <param name="isOn">暗くする場合はtrue</param>
    public void SetDarken(bool isOn)
    {
        _spriteRenderer.color = isOn
            ? new Color(_defaultColor.r * 0.4f, _defaultColor.g * 0.4f, _defaultColor.b * 0.4f, 1f)
            : _defaultColor;
    }

    public virtual void Effect(PreviewPlayerData preview)
    {
        Debug.Log("Effectメソッドが未実装");
    }

    public virtual void DestroyThis()
    {
        Debug.Log("DestroyThisメソッドが未実装");
    }

    /// <summary>
    ///         マウスオーバー時のパネル情報を取得する
    /// </summary>
    /// <returns></returns>
    public virtual PanelInfo GetPanelInfo()
    {
        return new PanelInfo("未設定", "-", "-");
    }

    /// <summary>
    /// Panelクラスを継承する場合、親クラスのAwake処理以外で何かしたい時、
    /// 子クラスにてCustomAwakeメソッドをoverrideして書く
    /// </summary>
    protected virtual void CustomAwake()
    {
        // 処理なし
    }
}
