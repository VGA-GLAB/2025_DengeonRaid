using UnityEngine;
using UnityEngine.UI;

public class Figures : MonoBehaviour
{
    [SerializeField] Text _walletText;
    //�R�C���̏��L��
    public static int _wallet;
    //Player��HP
    public static int _playerHP;

    public bool _death;

    void Update()
    {
        _walletText.text= _wallet.ToString();
        if (_playerHP <= 0)
        {
            _death = true;
        }
        else
        {
            _death = false;
        }
    }
}
