using DG.Tweening;
using System;
using UnityEngine;

public class WipeInController : MonoBehaviour
{
    [SerializeField] private GameObject _wipeInImage;
    void Start()
    {
        _wipeInImage.transform.DOMoveY(2000f, 1f).SetEase(Ease.Linear).OnComplete(() => _wipeInImage.SetActive(false));
    }
}
