using UnityEngine;

public class CoinStealPanel : Panel
{
    [SerializeField]
    private PreviewPlayerData playerData;
    [SerializeField]
    private int _stealCoinAmount;
    public void StealCoin()
    {
        playerData.Money -= _stealCoinAmount;
    }
}
