using UnityEngine;

public class SwordPanel : Panel
{
    [SerializeField, Header("Playerが与えるダメージに対するバフ")]
    private int _damageBuff;
    public override void Effect()
    {
        //Playerの攻撃力に_daageBuffを乗算する処理を追加する
    }
}
