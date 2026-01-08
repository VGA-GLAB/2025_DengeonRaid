using System.Text;
using UnityEngine;

public class ShieldPanel : Panel
{
    [SerializeField, Header("シールド量")]
    private int _shield = 5;

    public override void Effect(PreviewPlayerData preview)
    {
        preview.Shield += _shield;
    }
    public override void DestroyThis()
    {
        CRIAudioManager.CRISEManager.Play("SE_ActionShield");
        DeletePanelEffectManager.Instance.OnOneEffectGenerate(this);

        ReferenceManager.Instance.BoardManager.RemovePanelFromBoard(this);
        Destroy(gameObject);
    }

    public override PanelInfo GetPanelInfo()
    {
        string panelName = "シールド";
        string description = "消した数分シールドゲージを回復する";
        string attributes = $"シールドを{_shield}回復";
        return new PanelInfo(panelName, description, attributes);
    }
}
