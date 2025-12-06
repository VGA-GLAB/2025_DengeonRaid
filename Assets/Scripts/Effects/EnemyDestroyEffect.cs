using DG.Tweening;
using UnityEngine;

public class EnemyDestroyEffect : MonoBehaviour
{
    [SerializeField,Header("移動速度")]
    private float _moveSpeed;
    [SerializeField, Header("経由地点")]
    private Transform viaPos;
    [SerializeField, Header("目標地点")]
    private Transform targetPos;
    public void EnemyDebrisMove()
    {
        transform.DOPath(new Vector3[] { viaPos.position, targetPos.position }, _moveSpeed, PathType.CatmullRom);
    }
}
