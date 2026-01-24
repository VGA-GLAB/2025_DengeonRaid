using UnityEngine;

public class CoinStealPanel : EnemyPanel
{
    [SerializeField]
    private PreviewPlayerData playerData;
    [SerializeField]
    private int _stealCoinAmount;
    [SerializeField]
    private int _skillInterval;

    private int _turnProgress;

    public void TurnEndAction()
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
        playerData.Money -= _stealCoinAmount;
        if (playerData.Money <= _stealCoinAmount)
        {
            playerData.Money -= playerData.Money;
        }
    }
}
