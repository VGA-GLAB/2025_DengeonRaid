using System.Text;
using UnityEngine;

public class PotionPanel : Panel
{
    [SerializeField,Header("回復量")]
    private int _heal;

    public override void Effect(PreviewPlayerData preview)
    {
        preview.Hp += _heal;
    }
    public override void DestroyThis()
    {
        DeletePanelEffectManager.Instance.OnOneEffectGenerate(this);
        CRIAudioManager.CRISEManager.Play("SE_ActionPotion");

        ReferenceManager.Instance.BoardManager.RemovePanelFromBoard(this);
        Destroy(gameObject);
    }

    public override PanelInfo GetPanelInfo()
    {
        string panelName = "ハート";
        string description = "たまにしか出てこない。HPを回復できる！";
        string attributes = $"HPを{_heal}回復";
        return new PanelInfo(panelName, description, attributes);
    }
}
