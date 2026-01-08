using System.Text;
using UnityEngine;

public class CoinPanel : Panel
{
    [SerializeField,Header("1つのパネルにつき何コイン手に入れるか")]
    private int _coin;

    public override void Effect(PreviewPlayerData preview)
    {
        preview.Money += _coin;
    }
    public override void DestroyThis()
    {
        DeletePanelEffectManager.Instance.OnOneEffectGenerate(this);
        CRIAudioManager.CRISEManager.Play("SE_ActionMoney");

        ReferenceManager.Instance.BoardManager.RemovePanelFromBoard(this);
        Destroy(gameObject);
    }

    public override PanelInfo GetPanelInfo()
    {
        string panelName = "お金";
        string description = "つなげて消すとお金がもらえる。ゲージが貯まると自分を強化できる！！";
        string attributes = $"お金を{_coin}獲得";
        return new PanelInfo(panelName, description, attributes);
    }
}
