using UnityEngine;

public class PanelResolvingController : MonoBehaviour
{
    private ReferenceManager _rm;
    private PanelResolver _resolver;

    private void Start()
    {
        _rm = ReferenceManager.Instance;
    }

    /// <summary>
    /// 現段階、プレビュー作成と適用を分ける仕組みがないのでまとめて呼び出すメソッドにしておく
    /// </summary>
    public void ProcessWrapped()
    {
        ProcessPanelResolvePreview();
        ApplyPanelResolvePreview();
    }
    /// <summary>
    /// パネル消去結果のプレビューを作成する
    /// </summary>
    public void ProcessPanelResolvePreview()
    {
        _resolver = new PanelResolver(_rm.PlayerController, _rm.BoardManager.SelectedStack);
        _resolver.ProcessPreview();
    }

    /// <summary>
    /// パネル消去結果のプレビューを反映する
    /// </summary>
    public void ApplyPanelResolvePreview()
    {
        _resolver.ApplyPreviews();
        _resolver = null;
    }

    /// <summary>
    /// 【未実装】プレビューを反映せずに捨てる
    /// </summary>
    public void DiscardPreview()
    {

    }
}
