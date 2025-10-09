using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionHandler : MonoBehaviour
{
    public void OnStartButtonClick()
    {
        GlobalStateManager.Instance.Gsm.ChangeState<SInGame>();
        SceneManager.LoadScene("02_TestInGameScene_Chou");
    }
}