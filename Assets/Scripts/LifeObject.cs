using DG.Tweening;
using UnityEngine;

public class LifeObject : MonoBehaviour
{
    [SerializeField] private float addValue = 1f;
    [SerializeField] private GameObject lifeObject;
    public float LifeValue => addValue;

    private void Start()
    {
        lifeObject.transform.DOLocalMoveY(1f, 1f).SetLoops(-1, LoopType.Yoyo);
    }
}
