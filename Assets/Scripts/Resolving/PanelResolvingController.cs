using UnityEngine;

public class PanelResolvingController : MonoBehaviour
{
    private ReferenceManager _rm;
    private PanelResolver _resolver;

    private void Start()
    {
        _rm = ReferenceManager.Instance;
        //_rm.Igsm.States[typeof(SIGEliminatePanel)].OnEnter += ProcessWrapped;
    }

    /// <summary>
    /// 現段階、プレビュー作成と適用を分ける仕組みがないのでまとめて呼び出すメソッドにしておく
    /// </summary>
    public void ProcessWrapped()
    {
        ProcessPanelResolvePreview();
        ApplyPanelResolvePreview();
        //_rm.GameDirector.PanelResolvingFinished();
    }
    /// <summary>
    /// パネル消去結果のプレビューを作成する
    /// </summary>
    public void ProcessPanelResolvePreview()
    {
        _resolver = new PanelResolver(_rm.PlayerController, _rm.ChouBoardManager.SelectedStack);
        _resolver.ProcessPreview();
    }

    /// <summary>
    /// パネル消去結果のプレビューを反映する
    /// </summary>
    public void ApplyPanelResolvePreview()
    {
        _resolver.ApplyPreviews();
        //_rm.ChouBoardManager._selectedStack.Clear();
        _resolver = null;
    }

    /// <summary>
    /// 【未実装】プレビューを反映せずに捨てる
    /// </summary>
    public void DiscardPreview()
    {

    }
}
