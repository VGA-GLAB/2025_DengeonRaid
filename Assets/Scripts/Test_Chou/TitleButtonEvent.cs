using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButtonEvent : MonoBehaviour
{
    public void OnTitleButtonClicked()
    {
        SceneManager.LoadScene("01_TestTitleScene_Chou");
    }
}