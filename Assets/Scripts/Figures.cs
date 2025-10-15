using UnityEngine;
using UnityEngine.UI;

public class Figures : MonoBehaviour
{
    [SerializeField] Text _walletText;
    //ƒRƒCƒ“‚ÌŠ—L”
    public static int _wallet;
    //Player‚ÌHP
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
