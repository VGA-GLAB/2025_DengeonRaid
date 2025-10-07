using UnityEngine;

/// <summary>
///         盤面の情報を管理するクラス
/// </summary>
public class BoardManager : MonoBehaviour
{
    [Header("盤面設定")]
    [SerializeField] private int _width = 6;
    [SerializeField] private int _height = 6;

    [Header("参照")]
    [SerializeField] private Panel[] _panelPrefabs;
    [SerializeField][Tooltip("生成したパネルの親")] private Transform _boardRoot;

    private Panel[,] _boardArray;

    private void Start()
    {
        InitBoard();
    }

    //  盤面の初期化
    private void InitBoard()
    {
        _boardArray = new Panel[_width, _height];

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                int randomPanel = Random.Range(0, _panelPrefabs.Length);
                Panel panel = Instantiate(_panelPrefabs[randomPanel], _boardRoot);

                panel.transform.localPosition = new Vector3(x, -y, 0);
                _boardArray[x, y] = panel;
            }
        }
    }
}
