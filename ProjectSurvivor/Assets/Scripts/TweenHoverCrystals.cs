using UnityEngine;
using DG.Tweening;

public class TweenHoverCrystals : MonoBehaviour
{
    [SerializeField]
    private Transform[] crystals;

    private void Start() {
        Sequence sequence = DOTween.Sequence();

        foreach (var crystal in crystals)
        {
            sequence.Append(crystal.DOLocalMoveY(0.3f, 1f)).SetLoops(-1, LoopType.Yoyo);
        }
    }
}
