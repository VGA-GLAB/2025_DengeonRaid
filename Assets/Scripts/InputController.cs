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

    private SwipeAction _swipeAction;
    private InputAction _pressAction;
    private InputAction _positionAction;
    private bool _isDragging = false;
    private InGameStateMachine _gameStateMachine;

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
        _gameStateMachine = InGameStateManager.Instance.IGsm;
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
        Vector2 mousePos = _positionAction.ReadValue<Vector2>();
        Panel panel = GetPanelUnderCursor(mousePos);
        if (panel != null)
        {
            _isDragging = true;
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
        if (_isDragging)
        {
            _isDragging = false;
            if (_gameStateMachine.CurrentState is SIGDrawLine)
            {
                _gameStateMachine.ChangeState<SIGEliminatePanel>();
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
}
