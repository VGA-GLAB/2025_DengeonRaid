using UnityEngine;

/// <summary>
///         パネルのグループ分類を定義
///         一緒に消せるパネルの種類を判定するために使用
/// </summary>
public enum PanelGroup
{
    /// <summary>
    ///         通常パネル、同じID同士のみ接続可能
    /// </summary>
    Normal = 0,

    /// <summary>
    ///         バトル系、同グループなら異IDでも接続可能
    /// </summary>
    Battle = 1,

    /// <summary>
    ///         その他、必要があるなら名前変えて定義して
    /// </summary>
    Other = 2,
}
