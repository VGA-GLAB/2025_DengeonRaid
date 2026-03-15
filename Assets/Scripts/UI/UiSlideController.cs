using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///     操作説明のスライドコントローラー
/// </summary>
public class UiSlideController : MonoBehaviour
{
    [SerializeField] private RectTransform _content;
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _previousButton;
    [SerializeField] private float _pageWidth;
    [SerializeField] private float _slideDuration;
    [SerializeField] private Ease _ease;

    private int _currentPageIndex;
    private int _pageCount;
    private Tween _tween;

    /// <summary>
    ///     次のページへ移動
    /// </summary>
    public void NextPage()
    {
        if(_currentPageIndex < _pageCount - 1)
            SwichPage(++_currentPageIndex);
    }

    /// <summary>
    ///     前のページへ移動
    /// </summary>
    public void PreviousPage() 
    {
        if(_currentPageIndex > 0)
            SwichPage(--_currentPageIndex);
    }
    
    /// <summary>
    ///     指定したページへ移動
    /// </summary>
    /// <param name="pageIndex">移動先のページインデックス</param>
    public void SwichPage(int pageIndex)
    {
        if (pageIndex < 0 || pageIndex >= _pageCount)
            return;

        _currentPageIndex = pageIndex;
        Vector2 targetPosition = new Vector2(-_currentPageIndex * _pageWidth, _content.anchoredPosition.y);

        _tween?.Kill();
        _tween = _content.DOAnchorPos(targetPosition, _slideDuration)
            .SetEase(_ease);

        UpdateButtons();
    }

    /// <summary>
    ///     ボタンの状態を更新
    /// </summary>
    private void UpdateButtons()
    {
        _previousButton.interactable = _currentPageIndex > 0;
        _nextButton.interactable = _currentPageIndex < _pageCount - 1;     
    }

    private void Start()
    {
        _pageCount = _content.childCount;
        UpdateButtons();
    }
}
