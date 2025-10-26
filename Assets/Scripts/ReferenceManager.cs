using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ゲーム内あらゆるobjectの参照を集めるstaticクラス
/// 他のクラスで「ReferenceManager.Instance.XXX」を書くだけで参照が使える。Inspectorでの設定は不要になる
/// アクセスが自由すぎるので、注意が必要
/// </summary>
public class ReferenceManager : StaticInstanceMonoBehaviour<ReferenceManager>
{
    [Header("UI系")]
    public UIController UIController;
    public TextMeshProUGUI UIText;
    public Text UIPlayerHp;
    public Text UIArmor;
    public Text UIArmorExp;
    public Text UIExp;
    public Text UIGold;
    
    [Header("システム系")]
    public Camera MainCamera;
    public InputController InputController;
    public BoardManager BoardManager;
    public EventBus EventBus;
    public InGameStateMachine Igsm;
    public GameDirector GameDirector;
    public PanelResolvingController PanelResolvingController;
    public EnemyAttackController EnemyAttackController;

    [Header("ゲーム内要素")]
    public PlayerController PlayerController;
    public Transform BoardPosition;

    [Header("一時")]
    public ChouInputController ChouInputController;
    public ChouBoardManager ChouBoardManager;
}