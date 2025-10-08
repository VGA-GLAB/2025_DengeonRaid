using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private InGameStateManager _igsm;

    public void UpdateScoreText()
    {
        _scoreText.text = "Score: " + _igsm.Score.ToString();
    }
}