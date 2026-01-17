using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelInfoController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _panelName;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private TextMeshProUGUI _attributes;

    private void Start()
    {
        if(isActiveAndEnabled) gameObject.SetActive(false);
    }
    public void SetEnemyInfoText(Panel panel)
    {
        PanelInfo panelInfo = panel.GetPanelInfo();
        _panelName.text = panelInfo.PanelName;
        _description.text = panelInfo.Description;
        _attributes.text = panelInfo.AttributeText;
    }
}
