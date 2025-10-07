using UnityEngine;

/// <summary>
///         �Ֆʂ̏����Ǘ�����N���X
/// </summary>
public class BoardManager : MonoBehaviour
{
    [Header("�Ֆʐݒ�")]
    [SerializeField] private int _width = 6;
    [SerializeField] private int _height = 6;

    [Header("�Q��")]
    [SerializeField] private Panel[] _panelPrefabs;
    [SerializeField][Tooltip("���������p�l���̐e")] private Transform _boardRoot;

    private Panel[,] _boardArray;

    private void Start()
    {
        InitBoard();
    }

    //  �Ֆʂ̏�����
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
