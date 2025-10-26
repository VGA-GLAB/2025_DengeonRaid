using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventBus
{
    public Dictionary<string, Action<object>> Events;
    
    public EventBus()
    {
        Events = new Dictionary<string, Action<object>>();
    }

    /// <summary>
    /// キーを指定して、そのキー登録されているeventに処理を登録する
    /// </summary>
    /// <param name="key">eventキー</param>
    /// <param name="callback">処理</param>
    public void Subscribe(string key, Action<object> callback)
    {
        if (Events.ContainsKey(key))
        {
            Events[key] += callback;
        }
        else
        {
            Events.Add(key, callback);
        }
    }

    /// <summary>
    /// キーを指定して、そのキーで登録されているeventから処理を除去する
    /// </summary>
    /// <param name="key">eventキー</param>
    /// <param name="callback">処理</param>
    public void Unsubscribe(string key, Action<object> callback)
    {
        if (Events.ContainsKey(key))
        {
            Events[key] -= callback;
        }
        else
        {
            Debug.LogWarning("eventの登録解除：指定されたeventキーが存在しない");
        }
    }

    /// <summary>
    /// キーを指定して、そのキーで登録されているeventを実行する
    /// </summary>
    /// <param name="key">eventキー</param>
    /// <param name="param">引数</param>
    public void Punlish(string key, object param = null)
    {
        Events[key].Invoke(param);
    }
}
