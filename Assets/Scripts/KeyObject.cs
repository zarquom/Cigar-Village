using DG.Tweening;
using UnityEngine;

public class KeyObject : MonoBehaviour
{
    [SerializeField] private GameObject keyObject;
    private void Start()
    {
        keyObject.transform.DOLocalMoveZ(-1.6f, 1f).SetLoops(-1, LoopType.Yoyo);
    }
}
