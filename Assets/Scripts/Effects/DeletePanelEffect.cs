using DG.Tweening;
using System;
using UnityEngine;

/// <summary>
///         パネル演出を実行するクラス
/// </summary>
public class DeletePanelEffect : MonoBehaviour
{

    [SerializeField, Header("移動時に縮小する時のパネルのサイズ")]
    private float _panelMoveMinimalizeSize;

    [SerializeField, Header("移動時に拡大する時のパネルのサイズ")]
    private float _panelExpantionSize;

    [SerializeField, Header("エフェクトの総再生時間")]
    private float _effectPlayTime;

    [SerializeField, Header("再生時間ランダム化のブレ範囲"), Range(0f, 0.5f)]
    private float _playTimeFluctuation;

    [SerializeField, Header("移動の路線の曲がり具合"), Range(0f, 5f)]
    private float _pathBend;

    /// <summary>
    ///         消去したパネルをアニメーションで移動させる
    /// </summary>
    public void PanelMove(Transform targetObj, Panel panel, Action callBack)
    {
        // エフェクト再生時間にランダム補正を入れる
        float playTime = _effectPlayTime + UnityEngine.Random.Range(-_playTimeFluctuation, _playTimeFluctuation);

        // 移動路線の経由ポイントの座標を算出する
        Vector3 startPos = transform.position;
        Vector3 endPos = targetObj.position;
        float viaPosX = startPos.x + (endPos.x - startPos.x) / 2f;
        float viaPosY = startPos.y + (endPos.y - startPos.y) / 2f;
        float viaPosZ = startPos.x + (endPos.z - startPos.z) / 2f;
        viaPosY += targetObj.position.y > transform.position.y ? -_pathBend : _pathBend;

        Sequence seq = DOTween.Sequence();
        // Joinで追加されたDOTweenアニメーションは同時に実行される
        // Insertで追加されたDOTweenアニメーションは、指定された時間で実行される
        // Appendで追加されたDOTweenアニメーションは、前のアニメーションが終わってから実行される。ここでは使わないようにした
        // DOPathでは複数のpositionを指定でき、路線種類を「PathType.CatmullRom」に指定すると、指定されたpositionで自動的に曲線を描く
        seq.Join(transform.DOPath(new Vector3[] { transform.position, new Vector3(viaPosX, viaPosY, viaPosZ), targetObj.position }, playTime, PathType.CatmullRom))
            .Insert(0, transform.DOScale(_panelExpantionSize, playTime / 2).SetEase(Ease.OutQuart))
            .Insert(playTime / 2, transform.DOScale(_panelMoveMinimalizeSize, playTime / 2).SetEase(Ease.InQuart))
            .OnComplete(() =>
            {
                if (panel is ShieldPanel)
                    CRIAudioManager.CRISEManager.Play("SE_ActivationShield");

                callBack.Invoke();
                Destroy(gameObject);
            });
        seq.Play();
    }
}
