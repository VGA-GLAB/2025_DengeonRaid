using System;
using Unity.VisualScripting;
using UnityEngine;

public class PanelMovement : MonoBehaviour
{
    public bool InPosition;
    public float DropSpeed;
    [SerializeField] private Transform _panelPosition;
    private InGameStateMachine _sm;
    
    void Start()
    {
        InPosition = false;
        _sm = InGameStateManager.Instance.IGsm;
        // 状態のeventに、処理を登録する。これで状態が遷移する時、自動的に呼び出される。
        _sm.States[typeof(SIGEliminatePanel)].OnEnter += EliminatePanel;
    }

    private void OnDestroy()
    {
        // gameObjectが破棄される時、状態のeventから外すことを忘れずに
        _sm.States[typeof(SIGEliminatePanel)].OnEnter -= EliminatePanel;
    }

    private void FixedUpdate()
    {
        if (!InPosition)
        {
            DropToPosition();
        }
    }

    private void DropToPosition()
    {
        transform.position = Vector3.MoveTowards(
            transform.position, _panelPosition.position, DropSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, _panelPosition.position) <= 0.1f)
        {
            OnPanelInPosition();
        }
    }

    private void OnPanelInPosition()
    {
        InPosition = true;
        _sm.ChangeState<SIGCheckGameEnd>();
    }

    /// <summary>
    /// パネル消すのマネをする処理
    /// </summary>
    public void EliminatePanel()
    {
        if (InPosition)
        {
            transform.position += Vector3.up * 3f;
            InPosition = false;
            InGameStateManager.Instance.Score++;
        }
    }
}