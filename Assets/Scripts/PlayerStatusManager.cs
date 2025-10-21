using UnityEngine;
using System;

/// <summary>
///         プレイヤーの情報管理
/// </summary>
public class PlayerStatusManager : MonoBehaviour
{
    [SerializeField] private int _initMaxHp = 10;
    [SerializeField] private int _initMaxCoin = 10;
    [SerializeField] private int _initMaxShild = 10;

    private int _currentHp;
    private int _maxHp;
    private int _currentCoin;
    private int _currentShield;
    private int _maxShield;

    private void Start()
    {
        _maxHp = _initMaxHp;
        _currentHp = _initMaxHp;
        _maxShield = _initMaxShild;
        _currentShield = 0;
        _currentCoin = 0;
    }
     
    //  各値を取得できるメソッド群
    public Func<int> GetCurrentHp => () => _currentHp;
    public Func<int> GetMaxHp => () => _maxHp;

    public Func<int> GetCurrentCoin => () => _currentCoin;
    public Func<int> GetMaxCoin => () => _initMaxCoin;

    public Func<int> GetCurrentShield => () => _currentShield;
    public Func<int> GetMaxShield => () => _maxShield;

    /// <summary>
    ///         プレイヤーにシールド→HPの順でダメージを与える
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        if (_currentShield > 0)
        {
            int shildDameage = Mathf.Min(damage, _currentShield);
            _currentShield -= shildDameage;
            damage -= shildDameage;
        }

        if (damage > 0)
        {
            _currentHp -= damage;
            //  HPが0にならないように調整
            _currentHp = Mathf.Max(_currentHp, 0);
        }

        if (_currentHp <= 0)
        {
            //  0になった時にステートを変えるなど制御を追加予定
        }
    }

    /// <summary>
    ///         アイテムを買う際のコイン管理
    /// </summary>
    /// <param name="amount"></param>
    public void BuyItem(int amount)
    {
        _currentCoin -= amount;
        //TODO:金額が超えたときどうしよう
    }

    /// <summary>
    ///         レベルアップ
    /// </summary>
    public void LevelUp()
    { 
        //  あげる値考えよう
    }
}
