using UnityEngine;

public class MouseListener : MonoBehaviour
{
    private InGameStateMachine _sm;
    [SerializeField] private GameObject UIVignette;
    
    void Start()
    {
        _sm = InGameStateManager.Instance.IGsm;
        // lambda式でeventを登録するパターン。この方法で登録されたeventは後で除去できなくなるので、推奨しない。
        // なぜ今回使ったというと、例を挙げるためと、除去する必要がないからです。
        _sm.States[typeof(SIGDrawLine)].OnEnter += () =>{ UIVignette.SetActive(true); };
        _sm.States[typeof(SIGDrawLine)].OnExit += () =>{ UIVignette.SetActive(false); };
    }

    void Update()
    {
        if (_sm.CurrentState is SIGBusy) return;
        if (Input.GetMouseButtonDown(0))
        {
            _sm.ChangeState<SIGDrawLine>();
        }

        if (Input.GetMouseButtonUp(0))
        {
            UIVignette.SetActive(false);
            _sm.ChangeState<SIGEliminatePanel>();
        }
    }
}