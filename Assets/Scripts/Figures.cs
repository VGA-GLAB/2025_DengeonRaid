using UnityEngine;

public class Figures : MonoBehaviour
{
    //ƒRƒCƒ“‚ÌŠ—L”
    public static int _wallet;
    //Player‚ÌHP
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
