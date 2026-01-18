using System.Text;
using UnityEngine;

public class SwordPanel : Panel
{
    [SerializeField, Header("パネルごとのプレイヤー武器攻撃力加算数")]
    private int _weaponAmount;

    public override void Effect(PreviewPlayerData preview)
    {
        preview.WeaponAmount += _weaponAmount;
    }
    public override void DestroyThis()
    {
        CRIAudioManager.CRISEManager.Play("SE_ActionRocket");
        ReferenceManager.Instance.BoardManager.RemovePanelFromBoard(this);
        Destroy(gameObject);
    }

    public override PanelInfo GetPanelInfo()
    {
        string panelName = "ミサイル";
        string description = "敵とつなげるとダメージにも追加される！！";
        string attributes = $"追加攻撃力：{_weaponAmount}";
        return new PanelInfo(panelName, description, attributes);
    }
}
