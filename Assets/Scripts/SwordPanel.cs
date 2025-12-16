using UnityEngine;

public class SwordPanel : Panel
{
    [SerializeField, Header("パネルごとのプレイヤー武器攻撃力加算回数")]
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
}
