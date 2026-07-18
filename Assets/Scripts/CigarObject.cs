using DG.Tweening;
using UnityEngine;

public class CigarObject : MonoBehaviour
{
    [SerializeField] private float cigarValue = 1f;
    [SerializeField] private GameObject cigarObject;
    public float CigarValue => cigarValue;

    private void Start()
    {
        cigarObject.transform.DOLocalMoveZ(-1.6f, 1f).SetLoops(-1, LoopType.Yoyo);
    }
}
