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

    #region ライフサイクル
    private void Awake()
    {
        //  初期化
        _swipeAction = new SwipeAction();

        _pressAction = _swipeAction.GamePlay.Press;
        _positionAction = _swipeAction.GamePlay.Position;
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

    private void OnDestroy()
    {
        _pressAction.performed -= OnPress;
        _pressAction.canceled -= OnRelease;
        _swipeAction.Disable();
    }
    #endregion

    //  クリックした時
    private void OnPress(InputAction.CallbackContext context)
    {
        Vector2 mousePos = _positionAction.ReadValue<Vector2>();
        Panel panel = GetPanelUnderCursor(mousePos);
        if (panel != null)
        {
            _isDragging = true;
            if (panel != null)
            {
                _boardManager.StartSelection(panel);
            }
        }
    }

    //  クリックから離した時
    private void OnRelease(InputAction.CallbackContext ctx)
    {
        if (_isDragging)
        {
            _isDragging = false;
            _boardManager.EndSelection();
        }
    }

    //  マウス位置からRayを飛ばして、当たったパネルを返す
    private Panel GetPanelUnderCursor(Vector2 mousePos)
    {
        Vector3 worldPos = _camera.ScreenToWorldPoint(mousePos);
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

        return hit.collider ? hit.collider.GetComponent<Panel>() : null;
    }

    #region 登録と解除
    private void OnEnable()
    {
        _pressAction.performed += OnPress;
        _pressAction.canceled += OnRelease;

        _swipeAction.Enable();
    }

    private void OnDisable()
    {
        _pressAction.performed -= OnPress;
        _pressAction.canceled -= OnRelease;
        _swipeAction.Disable();
    }
    #endregion
}
