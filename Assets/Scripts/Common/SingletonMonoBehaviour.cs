using UnityEngine;

/// <summary>
/// シングルトーンコンポーネントを作る時に使う親クラス。
/// 《Level up your code with game programming patterns》を参考しています
/// </summary>
/// <typeparam name="T">子クラスの型</typeparam>
public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance { get; private set; }
    protected void Awake()
    {
        RemoveDuplicates();
        // CustomAwakeをオーバーライドして、子クラス固有のAwake処理を実装する
        CustomAwake();
    }
    protected void RemoveDuplicates()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// 継承する時、子クラスの固有Awake処理はこのメソッドをオーバーライドして書く
    /// </summary>
    protected virtual void CustomAwake()
    {
    }
}