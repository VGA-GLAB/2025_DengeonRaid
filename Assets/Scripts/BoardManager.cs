using System.Collections.Generic;
using UnityEngine;

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
    private InGameStateMachine _gameStateMachine;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 0;
    }

    private void Start()
    {
        InitBoard();
        _gameStateMachine = InGameStateManager.Instance.IGsm;
        _gameStateMachine.States[typeof(SIGEliminatePanel)].OnEnter += EndSelection;
        _gameStateMachine.States[typeof(SIGSpawnNewPanel)].OnEnter += DropPanel;
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
        //  選択中、パネルが違う種類,縦横斜めにない場合
        if (!_isSelected ||
            panel.PanelId != _selectedStack.Peek().PanelId ||
            !IsAdjacent8(_selectedStack.Peek(), panel))
            return;

        if (_selectedStack.Contains(panel))
        {
            if (panel == _selectedStack.Peek()) return;

            //  選択済みなら戻り処理
            while (_selectedStack.Peek() != panel)
            {
                Panel removed = _selectedStack.Pop();
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

        if (_selectedStack.Count >= _selectCount)
        {
            Debug.Log($"パネルを消去{_selectedStack.Count}個");

            foreach (var selectPanel in _selectedStack)
            {
                Vector2Int pos = selectPanel.BoardPos;
                _boardArray[pos.x, pos.y] = null;
                Destroy(selectPanel.gameObject);
            }
            _selectedStack.Clear();
        }

        _gameStateMachine.ChangeState<SIGSpawnNewPanel>();
        ClearLine();
    }


    /// <summary>
    ///         盤面の初期化
    /// </summary>
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

                panel.Initialize(new Vector2Int(x, y), randomPanel);
                _boardArray[x, y] = panel;
            }
        }
    }

    /// <summary>
    ///         パネル落としの処理
    /// </summary>
    private void DropPanel()
    {
        //  盤面の各列を左から順に処理
        for (int x = 0; x < _width; x++)
        {
            //  パネルを落とす先の位置を示す変数
            int emptyY = _height - 1;

            for (int y = _height - 1; y >= 0; y--)
            {
                Panel panel = _boardArray[x, y];
                //  このマスが空ならスキップ
                if (panel == null) continue;

                if (emptyY != y)
                {
                    //  盤面配列の更新
                    _boardArray[x, emptyY] = panel;
                    _boardArray[x,y] = null;

                    panel.BoardPos = new Vector2Int(x, emptyY);
                    panel.transform.localPosition = new Vector3Int(x, -emptyY, 0);
                }
                emptyY--; 
            }

            //  落とし終わったあと、上の方に空きが残っていれば新しいパネルを生成
            for (int y = emptyY; y >= 0; y--)
            {
                int randomPanel = Random.Range(0, _panelPrefabs.Length);
                Panel newPanel = Instantiate(_panelPrefabs[randomPanel], _boardRoot);

                //  TODO: 落下アニメーションをつける
                newPanel.transform.localPosition = new Vector3(x, -y, 0);
                newPanel.Initialize(new Vector2Int(x, y), randomPanel);
                _boardArray[x, y] = newPanel;
            }
        }
        _gameStateMachine.ChangeState<SIGIdle>();
    }

    /// <summary>
    ///         ライン更新
    /// </summary>
    private void UpdateLine()
    {
        //  一度選択した線が、次のドラッグでも残るためすべて消去
        _linePositions.Clear();

        foreach (var panel in _selectedStack)
        {
            if (panel != null)
                _linePositions.Add(panel.transform.position);
        }

        _lineRenderer.positionCount = _linePositions.Count;
        _lineRenderer.SetPositions(_linePositions.ToArray());
    }

    /// <summary>
    ///         ライン初期化
    /// </summary>
    private void ClearLine()
    {
        _lineRenderer.positionCount = 0;
        _linePositions.Clear();
    }

    /// <summary>
    ///         縦横斜めと接しているかの判定
    /// </summary>
    /// <returns>縦横斜めで隣接していたらtrue</returns>
    private bool IsAdjacent8(Panel a, Panel b)
    {
        Vector2Int posA = a.BoardPos;
        Vector2Int posB = b.BoardPos;
        int dx = Mathf.Abs(posA.x - posB.x);
        int dy = Mathf.Abs(posA.y - posB.y);

        return (dx <= 1 && dy <= 1 && (dx + dy != 0));
    }
}
