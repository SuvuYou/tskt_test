using System;
using DG.Tweening;
using UnityEngine;

public class SmokeParticle : MonoBehaviour
{
    public void Initialize(Vector3[] path, float duration, float endScale, Action<SmokeParticle> onComplete)
    {
        transform.DOPath(path, duration, PathType.CatmullRom)
            .SetEase(Ease.Linear)
            .OnComplete(() => onComplete(this));

        transform.DOScale(endScale, duration).SetEase(Ease.InCubic);
    }
}