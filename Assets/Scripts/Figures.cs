using UnityEngine;

public class Figures : MonoBehaviour
{
    //�R�C���̏��L��
    public static int _wallet;
    //Player��HP
    public static int _playerHP;
    
    public bool _death;

    void Update()
    {
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
