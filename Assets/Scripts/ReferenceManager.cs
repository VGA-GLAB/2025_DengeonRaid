using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ゲーム内あらゆるobjectの参照を集めるstaticクラス
/// 他のクラスで「ReferenceManager.Instance.XXX」を書くだけで参照が使える。Inspectorでの設定は不要になる
/// アクセスが自由すぎるので、注意が必要
/// </summary>
[DefaultExecutionOrder(-1)]
// DefaultExecutionOrder：コンポーネントの起動順を指定する。デフォルト値は0。
// これを-1に設定することによって、ReferenceManagerのAwakeが他のコンポーネントより先に実行されるようになり、
// 他のコンポーネントのAwakeメソッドで呼び出される時、Instanceが初期化済みであることを確保できる。
public class ReferenceManager : StaticInstanceMonoBehaviour<ReferenceManager>
{
    [Header("UI系")]
    public UIController UIController;
    public Text UIPlayerHpText;
    public Slider UIPlayerHpGuage;
    public Text UIPlayerShieldText;
    public Slider UIPlayerShieldGuage;
    public Text UIExp;
    public Text UIMoneyText;
    public Slider UIMoneyGuage;
    public Text UIBaseAttackText;
    public Text UIMissileAttackText;
    public GameObject UIEnemyAttackPanel;
    public TextMeshProUGUI UIEnemyDamageText;
    public GameObject UIWipeImage;
    public GameObject ShopUI;

    [Header("システム系")]
    public Camera MainCamera;
    public InputController InputController;
    public BoardManager BoardManager;
    public EventBus EventBus;
    public InGameStateMachine Igsm;
    public GameDirector GameDirector;
    public PanelResolvingController PanelResolvingController;
    public EnemyAttackController EnemyAttackController;
    public ShopUIManager ShopUIManager;

    [Header("ゲーム内要素")]
    public PlayerController PlayerController;
    public Transform BoardPosition;
    public BossPanel BossPanel;
}