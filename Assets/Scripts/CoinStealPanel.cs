using UnityEngine;

public class CoinStealPanel : EnemyPanel
{
    [SerializeField]
    private int _stealCoinAmount;
    [SerializeField]
    private int _skillInterval;

    private int _turnProgress;

    private ReferenceManager _rm;

    private PlayerController _playerController;

    private PreviewPlayerData _playerData;

    protected override void EnemyCustomStart()
    {
        _rm = ReferenceManager.Instance;
        _playerController = _rm.PlayerController;
        _playerData = new PreviewPlayerData(_playerController);
        _rm.Igsm.States[typeof(SIGEnemyTurn)].OnExit += OnTurnEndAction;
    }

    private void OnTurnEndAction()
    {
        _turnProgress++;
        if (_turnProgress >= _skillInterval)
        {
            StealCoin();
            _turnProgress = 0;
        }
    }

    public void StealCoin()
    {
        _playerData.Money = Mathf.Max(0, _playerData.Money - _stealCoinAmount);

        _playerController.SetResolvePreview(_playerData);
        _playerController.ApplyPreview();
    }



    private void OnDestroy()
    {
        _rm.Igsm.States[typeof(SIGEnemyTurn)].OnExit -= OnTurnEndAction;
    }
}
