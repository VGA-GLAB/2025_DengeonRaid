using UnityEngine;

public class Figures : MonoBehaviour
{
    //ƒRƒCƒ“‚ÌŠ—L”
    public int Wallet;
    //Player‚ÌHP
    public int PlayerHP;

    public bool IsDeath;

    void Update()
    {
        if (PlayerHP <= 0)
        {
            IsDeath = true;
        }
        else
        {
            IsDeath = false;
        }
    }
}
