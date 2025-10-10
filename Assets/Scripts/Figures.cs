using UnityEngine;

public class Figures : MonoBehaviour
{
    //ƒRƒCƒ“‚ÌŠ—L”
    public int _wallet;
    //Player‚ÌHP
    public int _playerHP;
    
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
