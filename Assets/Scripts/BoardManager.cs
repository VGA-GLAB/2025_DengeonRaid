using UnityEngine;
using System.Collections.Generic;

/// <summary>
///         盤面の情報を管理するクラス
/// </summary>
public class BoardManager : MonoBehaviour
{
    [Header("何個以上選択したら消すか")]
    [SerializeField] private int _selectCount = 3;

    [Header("盤面設定")]
    [SerializeField] private int _width = 6;
    [SerializeField] private int _height = 6;

    [Header("参照")]
    [SerializeField] private Panel[] _panelPrefabs;
    [SerializeField, Tooltip("生成したパネルの親")] private Transform _boardRoot;

    private Panel[,] _boardArray;
    private Stack<Panel> _selectedStack = new Stack<Panel>();
    private bool _isSelected = false;

    private LineRenderer _lineRenderer;
    private List<Vector3> _linePositions = new List<Vector3>();

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 0;
    }

    private void Start()
    {
        InitBoard();
    }

    /// <summary>
    ///         指定した座標のパネルを取得
    /// </summary>
    public Panel GetPanel(int x, int y)
    {
        //　範囲チェック
        if (x < 0 || y < 0 || x >= _width || y >= _height) return null;
        return _boardArray[x, y];
    }

    /// <summary>
    ///         なぞり処理開始
    /// </summary>
    public void StartSelection(Panel panel)
    {
        Debug.Log("選択開始", panel);
        _selectedStack.Clear();
        _selectedStack.Push(panel);
        _isSelected = true;

        UpdateLine();
    }

    /// <summary>
    ///         ドラッグ中にパネルをなぞる
    /// </summary>
    public void ContinueSelection(Panel panel)
    {
        if (!_isSelected) return;

        if (_selectedStack.Contains(panel))
        {
            if (panel == _selectedStack.Peek()) return;

            //  選択済みなら戻り処理
            while (_selectedStack.Peek() != panel)
            {
                var removed = _selectedStack.Pop();
            }
            return;
        }
        else
        {
            _selectedStack.Push(panel);
        }

        UpdateLine();
    }

    /// <summary>
    ///         なぞり処理終了
    /// </summary>
    public void EndSelection()
    {
        if (!_isSelected) return;
        _isSelected = false;

        if (_selectedStack.Count > _selectCount)
        {
            Debug.Log($"パネルを消去{_selectedStack.Count}個");

            foreach (var selectPanel in _selectedStack)
            {
                Destroy(selectPanel.gameObject);
            }
            _selectedStack.Clear();
        }

        ClearLine();
    }


    //  盤面の初期化
    private void InitBoard()
    {
        _boardArray = new Panel[_width, _height];

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                //  後でランダムではなくして調整
                int randomPanel = Random.Range(0, _panelPrefabs.Length);
                Panel panel = Instantiate(_panelPrefabs[randomPanel], _boardRoot);

                panel.transform.localPosition = new Vector3(x, -y, 0);
                _boardArray[x, y] = panel;
            }
        }
    }

    //  ライン更新
    private void UpdateLine()
    {
        //  一度選択した線が、次のドラッグでも残るためすべて消去
        _linePositions.Clear();

        foreach(var panel in _selectedStack)
        {
            if(panel != null)
                _linePositions.Add(panel.transform.position);
        }

        _lineRenderer.positionCount = _linePositions.Count;
        _lineRenderer.SetPositions(_linePositions.ToArray());
    }

    //  ライン初期化
    private void ClearLine()
    {
        _lineRenderer.positionCount = 0;
        _linePositions.Clear();
    }
}
