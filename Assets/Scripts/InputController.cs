using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
///         入力処理クラス
/// </summary>
public class InputController : MonoBehaviour
{
    [Header("参照")]
    [SerializeField] private BoardManager _boardManager;
    [SerializeField] private Camera _camera;
    [Header("パラメータ")]
    [SerializeField, Header("敵情報表示待ち時間")] private float _hoverUITime;

    private SwipeAction _swipeAction;
    private InputAction _pressAction;
    private InputAction _positionAction;
    private bool _isDragging = false;
    private InGameStateMachine _gameStateMachine;
    private ReferenceManager _rm;

    // マウスオーバー時間
    private float _hoverTimer;
    // 前フレームにマウスの下のパネル
    private Panel _lastPanelUnderCursor;

    #region ライフサイクル
    private void Awake()
    {
        //  初期化
        _swipeAction = new SwipeAction();

        _pressAction = _swipeAction.GamePlay.Press;
        _positionAction = _swipeAction.GamePlay.Position;
    }

    private void Start()
    {
        _rm = ReferenceManager.Instance;
        _gameStateMachine = _rm.Igsm;
    }

    private void Update()
    {
        if (_isDragging)
        {
            Vector2 mousePos = _positionAction.ReadValue<Vector2>();
            Panel panel = GetPanelUnderCursor(mousePos);
            if (panel != null)
            {
                _boardManager.ContinueSelection(panel);
            }
        }
        else
        {
            HandleHovering();
        }
    }

    private void OnEnable()
    {
        _pressAction.performed += HandlePress;
        _pressAction.canceled += HandleRelease;
        _swipeAction.Enable();
    }

    private void OnDisable()
    {
        _pressAction.performed -= HandlePress;
        _pressAction.canceled -= HandleRelease;
        _swipeAction.Disable();
    }

    private void OnDestroy()
    {
        _pressAction.performed -= HandlePress;
        _pressAction.canceled -= HandleRelease;
        _swipeAction.Disable();
    }
    #endregion

    /// <summary>
    ///         クリックした時
    /// </summary>
    private void HandlePress(InputAction.CallbackContext ctx)
    {
        _rm.UIEnemyInfo.gameObject.SetActive(false);
        Vector2 mousePos = _positionAction.ReadValue<Vector2>();
        Panel panel = GetPanelUnderCursor(mousePos);
        if (panel != null)
        {
            _isDragging = true;
            _boardManager.StartSelection(panel);
            if (_gameStateMachine.CurrentState is SIGIdle)
            {
                _gameStateMachine.ChangeState<SIGDrawLine>();
            }
        }
    }

    /// <summary>
    ///         クリックから離した時
    /// </summary>
    private void HandleRelease(InputAction.CallbackContext ctx)
    {
        if (!_isDragging) return;
        _isDragging = false;

        int selectedCount = _boardManager.SelectedCount;
        int neededCount = _boardManager.SelectThreshold;


        if (_gameStateMachine.CurrentState is SIGDrawLine)
        {
            if (selectedCount >= neededCount)
            {
                _gameStateMachine.ChangeState<SIGEliminatePanel>();
            }
            else
            {
                _gameStateMachine.ChangeState<SIGIdle>();
                _boardManager.ClearSelection();
            }
        }
    }

    /// <summary>
    ///          マウス位置からRayを飛ばして、当たったパネルを返す
    /// </summary>
    /// <param name="mousePos">マウスカーソルの位置</param>
    /// <returns>その位置のパネルを返す</returns>
    private Panel GetPanelUnderCursor(Vector2 mousePos)
    {
        Vector3 worldPos = _camera.ScreenToWorldPoint(mousePos);
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

        return hit.collider ? hit.collider.GetComponent<Panel>() : null;
    }

    /// <summary>
    ///         マウスオーバー処理
    /// </summary>
    private void HandleHovering()
    {
        // マウス位置
        Vector2 mousePos = _positionAction.ReadValue<Vector2>();
        // マウスの下のパネルを取得
        Panel panel = GetPanelUnderCursor(mousePos);
        if(_lastPanelUnderCursor == null) _lastPanelUnderCursor = panel;

        // マウスの下のパネルが敵、かつマウスが同じパネルに置き続けている場合
        if (panel is EnemyPanel && Object.ReferenceEquals(panel, _lastPanelUnderCursor))
        {
            _hoverTimer += Time.deltaTime;
            // タイマーが一定時間過ぎて、敵情報が表示されていない場合、表示処理を行う
            if (_hoverTimer >= _hoverUITime && !_rm.UIEnemyInfo.gameObject.activeInHierarchy)
            {
                _rm.UIController.ShowEnemyInfo(panel as EnemyPanel);
            }
        }
        else
        {
            // マウスが離れたら、タイマーをリセットして、情報を非表示にする
            _hoverTimer = 0;
            _lastPanelUnderCursor = panel;
            _rm.UIController.HideEnemyInfo();
        }
    }
}
