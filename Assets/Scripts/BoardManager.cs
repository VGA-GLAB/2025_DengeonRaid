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
    [SerializeField] private PanelWeightData[] _panelPrefabs;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private GameObject _arrowPrefab;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private PanelDropManager _panelDropManager;
    [SerializeField, Tooltip("生成したパネルの親")] private Transform _boardRoot;

    private GameObject _currentArrow;
    private Panel[,] _boardArray;
    private Stack<Panel> _selectedStack = new Stack<Panel>();
    private List<Panel> _highlightedPanels = new List<Panel>();
    private bool _isSelected = false;
    private bool _isSkillUsed = false;

    private List<Vector3> _linePositions = new List<Vector3>();
    private InGameStateMachine _gameStateMachine;
    private ReferenceManager _rm;
    private GameDirector _director;

    public Stack<Panel> SelectedStack { get { return _selectedStack; } }
    public Panel[,] GetBoardArray { get { return _boardArray; } }

    #region ライフサイクル
    private void Awake()
    {
        if (_lineRenderer == null)
        {
            if (!TryGetComponent(out _lineRenderer))
            {
                _lineRenderer = this.gameObject.AddComponent<LineRenderer>();
                Debug.LogWarning("LineRendererが見つからないので自動追加", this);
            }
        }

        _lineRenderer.positionCount = 0;
    }

    private void Start()
    {
        _rm = ReferenceManager.Instance;
        _director = _rm.GameDirector;
        InitBoard();
        _gameStateMachine = _rm.Igsm;
        _gameStateMachine.States[typeof(SIGEliminatePanel)].OnEnter += EndSelection;
        _gameStateMachine.States[typeof(SIGSpawnNewPanel)].OnEnter += DropPanel;

        destroyCancellationToken.Register(() =>
        {
            _gameStateMachine.States[typeof(SIGEliminatePanel)].OnEnter -= EndSelection;
            _gameStateMachine.States[typeof(SIGSpawnNewPanel)].OnEnter -= DropPanel;
        });
    }
    #endregion

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
    ///         ボードにある全ての敵パネルを取得
    /// </summary>
    /// <returns></returns>
    public List<EnemyPanel> GetEnemyPanels()
    {
        List<EnemyPanel> rst = new List<EnemyPanel>();
        foreach (var panel in _boardArray)
        {
            if (panel is EnemyPanel p)
            {
                rst.Add(p);
            }
        }
        return rst;
    }

    /// <summary>
    ///         指定した座標のパネルを置き換える
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="panel"></param>
    public void ReplacePanel(Vector2Int pos, Panel panel)
    {
        _boardArray[pos.x, pos.y].DestroyThis();
        //  新しいパネルを生成、初期化
        Panel newPanel = Instantiate(panel, _boardRoot);
        newPanel.transform.localPosition = new Vector3(pos.x, -pos.y, 0);
        newPanel.Initialize(new Vector2Int(pos.x, pos.y));
        _boardArray[pos.x, pos.y] = newPanel;
    }

    /// <summary>
    ///         指定した座標のパネルを削除する
    /// </summary>
    /// <param name="pos"></param>
    public void DeleatePanel(Vector2Int pos)
    {
        //  スキル使用フラグを立てて、パネル効果を発動
        _isSkillUsed = true;
        _boardArray[pos.x, pos.y].Effect(new PreviewPlayerData(_playerController));
        _boardArray[pos.x, pos.y].DestroyThis();
        DropPanel();
    }

    #region なぞり処理
    /// <summary>
    ///         なぞり処理開始
    /// </summary>
    public void StartSelection(Panel panel)
    {
        Debug.Log("選択開始", panel);
        _selectedStack.Clear();
        _selectedStack.Push(panel);
        _isSelected = true;

        ClearHighLight();
        _highlightedPanels.Clear();
        HighlightConnectablePanels(panel, panel);

        UpdateLine();
    }

    /// <summary>
    ///         ドラッグ中にパネルをなぞる
    /// </summary>
    public void ContinueSelection(Panel panel)
    {
        //  選択中、縦横斜めにない場合
        if (!_isSelected ||
            !IsAdjacent8(_selectedStack.Peek(), panel))
            return;

        Panel lastPanel = _selectedStack.Peek();

        //  接続可能かをチェック
        if (!CanConnectType(lastPanel, panel))
            return;

        if (_selectedStack.Contains(panel))
        {
            if (panel == _selectedStack.Peek()) return;

            //  選択済みなら戻り処理
            while (_selectedStack.Peek() != panel)
            {
                Panel removed = _selectedStack.Pop();
                UpdateLine();
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

        ClearHighLight();

        if (_selectedStack.Count >= _selectCount)
        {
            Debug.Log($"パネルを消去{_selectedStack.Count}個");

            foreach (var selectPanel in _selectedStack)
            {
                Vector2Int pos = selectPanel.BoardPos;
            }

            _rm.PanelResolvingController.ProcessWrapped();
            _selectedStack.Clear();
            _director.PanelResolvingFinished();
        }

        ClearLine();
    }
    #endregion

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
                Panel panel = Instantiate(GetRandomPanel(), _boardRoot);
                panel.transform.localPosition = new Vector3(x, -y, 0);

                panel.Initialize(new Vector2Int(x, y));
                _boardArray[x, y] = panel;
            }
        }
    }

    /// <summary>
    ///         パネル落としの処理
    /// </summary>
    private void DropPanel()
    {
        List<Panel> droppedPanels = new List<Panel>();

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
                    _boardArray[x, y] = null;

                    panel.BoardPos = new Vector2Int(x, emptyY);
                    panel.transform.localPosition = new Vector3Int(x, -emptyY, 0);
                }
                emptyY--;
            }

            //  落とし終わったあと、上の方に空きが残っていれば新しいパネルを生成
            for (int y = emptyY; y >= 0; y--)
            {
                Panel newPanel = Instantiate(GetRandomPanel(), _boardRoot);

                //  TODO: 落下アニメーションをつける
                newPanel.transform.localPosition = new Vector3(x, y + 10, 0);
                newPanel.Initialize(new Vector2Int(x, y));
                _boardArray[x, y] = newPanel;
                droppedPanels.Add(newPanel);
            }
        }

        _panelDropManager.DropAll(droppedPanels);

        if (!_isSkillUsed)
            _director.SpawnNewPanelFinished();

        _isSkillUsed = false;
    }

    /// <summary>
    ///         重み付きランダムでパネルを取得
    /// </summary>
    /// <returns></returns>
    private Panel GetRandomPanel()
    {
        int totalWeight = 0;
        foreach (var panelData in _panelPrefabs)
            totalWeight += panelData.Weight;

        int randomValue = Random.Range(0, totalWeight);
        int currentWeight = 0;

        //  ループで積み上げながら比較
        foreach (var panelData in _panelPrefabs)
        {
            currentWeight += panelData.Weight;
            if (randomValue < currentWeight)
            {
                return panelData.PanelPrefab;
            }
        }
        //  念のため返す
        return _panelPrefabs.Length > 0 ? _panelPrefabs[0].PanelPrefab : null;
    }

    #region LineRendrer関連
    /// <summary>
    ///         ライン更新
    /// </summary>
    private void UpdateLine()
    {
        if (_lineRenderer == null) return;
        //  一度選択した線が、次のドラッグでも残るためすべて消去
        _linePositions.Clear();

        foreach (var panel in _selectedStack)
        {
            if (panel != null)
                _linePositions.Add(panel.transform.position);
        }

        _lineRenderer.positionCount = _linePositions.Count;
        _lineRenderer.SetPositions(_linePositions.ToArray());

        UpdateArrowHead();
    }

    /// <summary>
    ///         ライン初期化
    /// </summary>
    private void ClearLine()
    {
        if (_lineRenderer == null) return;
        _lineRenderer.positionCount = 0;
        _linePositions.Clear();
        _currentArrow.SetActive(false);
    }

    /// <summary>
    ///         矢印の向きを更新
    /// </summary>
    private void UpdateArrowHead()
    {
        if (_linePositions.Count < 2)
        {
            if (_currentArrow != null)
                _currentArrow.SetActive(false);
            return;
        }

        //   終点と一つ前の点から向きを算出
        Vector3 end = _linePositions[0];
        Vector3 prev = _linePositions[1];

        Vector3 dir = (end - prev).normalized;
        //  始点から終点に向かうベクトルの角度を、0°〜360の見た目の回転角に変換
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        if (_currentArrow == null)
            _currentArrow = Instantiate(_arrowPrefab, _lineRenderer.transform);

        //   終点に設置
        _currentArrow.transform.SetPositionAndRotation(
            end,
            Quaternion.Euler(0, 0, angle)
        );

        _currentArrow.SetActive(true);
    }
    #endregion

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

    /// <summary>
    ///         GroupやIDを見てつなげられるか判定
    /// </summary>
    /// <returns></returns>
    private bool CanConnectType(Panel a, Panel b)
    {
        //  同じグループならOK
        if (a.Group == b.Group)
        {
            //  同グループ内で同じIDならOK
            if (a.PanelId == b.PanelId)
                return true;

            //  グループがBattleなら、異なるIDでもOK
            if (a.Group == PanelGroup.Battle)
                return true;
        }

        return false;
    }

    /// <summary>
    ///         選択できるものをハイライトするクラス
    /// </summary>
    /// <param name="rootPanel">マウスで最初に選んだパネル</param>
    /// <param name="startPanel">探索の中心パネル</param>
    private void HighlightConnectablePanels(Panel rootPanel, Panel startPanel)
    {
        startPanel.SetHighlight(true);
        _highlightedPanels.Add(startPanel);

        // 8方向探索
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                //  自分自身は除外
                if (dx == 0 && dy == 0) continue;

                int nx = startPanel.BoardPos.x + dx;
                int ny = startPanel.BoardPos.y + dy;

                Panel neighbor = GetPanel(nx, ny);
                if (neighbor == null) continue;

                //  現在のstartPanelとの接続条件
                if (CanConnectType(startPanel, neighbor))
                {
                    // 無限ループ防止
                    if (!ReferenceEquals(rootPanel, neighbor) &&
                        !_highlightedPanels.Contains(neighbor))
                    {
                        // 再帰呼び出し
                        HighlightConnectablePanels(rootPanel, neighbor);
                    }
                }
            }
        }
    }

    /// <summary>
    ///         ハイライトを消去
    /// </summary>
    private void ClearHighLight()
    {
        foreach (var panel in _highlightedPanels)
        {
            if (panel != null)
                panel.SetHighlight(false);
        }
        _highlightedPanels.Clear();
    }

    /// <summary>
    ///         パネル二次元配列にて、指定されたパネルをnullに設定する
    /// </summary>
    /// <param name="panel"></param>
    public void RemovePanelFromBoard(Panel panel)
    {
        _boardArray[panel.BoardPos.x, panel.BoardPos.y] = null;
    }
}
