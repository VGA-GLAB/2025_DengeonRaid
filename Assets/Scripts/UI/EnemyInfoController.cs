using UnityEngine;
using UnityEngine.UI;

public class EnemyInfoController : MonoBehaviour
{
    [SerializeField] private Text _text;

    private void Start()
    {
        if(isActiveAndEnabled) gameObject.SetActive(false);
    }
    public void SetEnemyInfoText(EnemyPanel enemy)
    {
        // 実装待ち
    }
}
