using System.Collections;
using TMPro;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    private ReferenceManager _rm;
    private EnemyAttackProcessor _processor;
    private WaitForSeconds _waitHalfSecond;

    [Header("Base Settings")]
    [SerializeField] private float _animationDuration = 0.5f;


    [Header("TMP Font Size Animation Settings")]
    [SerializeField, Header("拡大にかける時間")] private float _expandDuration = 0.5f;
    [SerializeField, Header(" かくかくの段数")] private int _steps = 2;
    [SerializeField, Header("最大フォントサイズ倍率")] private float _maxScaleFactor = 1.5f;
    [SerializeField, Header("収束にかける時間")] private float _returnWaitTime = 0.1f;

    private float _originalFontSize;

    #region ライフサイクル
    private void Start()
    {
        _rm = ReferenceManager.Instance;
        _rm.Igsm.States[typeof(SIGEnemyTurn)].OnEnter += ProcessEnemyAttack;
        _waitHalfSecond = new WaitForSeconds(0.5f);

        if (_rm.UIEnemyDamageText != null)
        {
            _originalFontSize = _rm.UIEnemyDamageText.fontSize;
            _rm.UIEnemyDamageText.fontSize = 0;
            _rm.UIEnemyAttackPanel.SetActive(false);
        }
    }
    #endregion

    /// <summary>
    /// 敵攻撃処理を実行する
    /// </summary>
    public void ProcessEnemyAttack()
    {
        CRIAudioManager.CRISEManager.Play("SE_EnemyAttack");
        StartCoroutine(ProcessCoroutine());
    }

    private IEnumerator ProcessCoroutine()
    {
        _processor = new EnemyAttackProcessor(_rm.BoardManager, _rm.PlayerController);

        if (_processor.EnemyExists())
        {
            _processor.Process();
            _rm.UIEnemyAttackPanel.SetActive(true);

            TMP_Text damageText = _rm.UIEnemyDamageText;
            damageText.text = "Damage : " + _processor.FinalDamage;

            yield return StartCoroutine(StepFontSizeExpandCo(damageText));


            yield return StartCoroutine(SnapReturnToOriginalSizeCo(damageText));

            yield return _waitHalfSecond;

            damageText.fontSize = _originalFontSize;
            _rm.UIEnemyAttackPanel.SetActive(false);

            if(_processor.FinalDamage > 0)
            {
                _rm.UIController.PlayHpGuageShake();
                if (_rm.PlayerController.Shield > 0)
                {
                    _rm.UIController.PlayShieldGuageShake();
                }
            }
            else
            {
                _rm.UIController.PlayShieldGuageShake();
            }

            _processor.ApplyDamageToPlayer();
        }

        _processor = null;
        _rm.GameDirector.EnemyAttackFinished();
    }

    /// <summary>
    /// フォントサイズを段階的に拡大する
    /// </summary>
    private IEnumerator StepFontSizeExpandCo(TMP_Text targetText)
    {
        float startTime = Time.time;
        float maxFontSize = _originalFontSize * _maxScaleFactor;

        while (Time.time < startTime + _expandDuration)
        {
            float elapsedTime = Time.time - startTime;
            float t = elapsedTime / _expandDuration;

            float steppedT = Mathf.Floor(t * _steps) / _steps;

            targetText.fontSize = Mathf.Lerp(0, maxFontSize, steppedT);

            yield return null;
        }
        targetText.fontSize = maxFontSize;
    }

    /// <summary>
    ///  元に戻るコルーチン
    /// </summary>
    private IEnumerator SnapReturnToOriginalSizeCo(TMP_Text targetText)
    {
        yield return new WaitForSeconds(_returnWaitTime);

        targetText.fontSize = _originalFontSize;

        yield return null;
    }
}