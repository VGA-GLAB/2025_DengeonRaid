using UnityEngine;

/// <summary>
/// コンポネントを静的インスタンスにしたい時の親クラス
/// </summary>
/// <typeparam name="T">子クラスの型</typeparam>
public class StaticInstanceMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T _instance;
    public static T Instance { get; private set; }

    protected void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning($"{typeof(T)}のインスタンスが2つ以上作成されています！参照の管理を確認してください。");
            Destroy(gameObject);
            return;
        }
        Instance = this as T;
        // CustomAwakeをオーバーライドして、子クラス固有のAwake処理を実装する
        CustomAwake();
    }

    protected virtual void OnDestroy()
    {
        // Scene切り替わる時、クラスの実例が消去されるが、実例への参照は未だ残っている。
        // 変なアドレスに参照しないように、Objectが消去される前にInstanceの参照をnullに設定する
        if (Instance == this) Instance = null;
    }

    /// <summary>
    /// 継承する時、子クラスの固有Awake処理はこのメソッドをオーバーライドして書く
    /// </summary>
    protected virtual void CustomAwake()
    {
    }
}