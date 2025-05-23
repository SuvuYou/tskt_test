using System.Collections;
using UnityEngine;
using DG.Tweening;

class HitIndicator : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private RectTransform _pivotPoint;

    [Header("Animation Settings")]
    [SerializeField] private float _maxScale = 2f;
    [SerializeField] private float _scaleUpDuration = 0.1f;
    [SerializeField] private float _scaleDownDuration = 0.4f;

    private Vector3 _originalScale;

    private void Awake()
    {
        _originalScale = _rectTransform.localScale;
    }

    public void Show() => _rectTransform.gameObject.SetActive(true);
    public void Hide() => _rectTransform.gameObject.SetActive(false);

    public void SetHitDirection(Vector3 hitDireciton) 
    {
        if (hitDireciton == Vector3.zero) return;
    
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, Vector3.SignedAngle(hitDireciton, _pivotPoint.forward, Vector3.up) + 135f);

        _pivotPoint.localRotation = targetRotation;

        ShowHit();
    }

    public void ShowHit()
    {
        _rectTransform.localScale = _originalScale;
        _rectTransform.DOKill();

        Sequence hitSequence = DOTween.Sequence();
        hitSequence.Append(_rectTransform.DOScale(_originalScale * _maxScale, _scaleUpDuration).SetEase(Ease.OutQuad));
        hitSequence.Append(_rectTransform.DOScale(_originalScale, _scaleDownDuration).SetEase(Ease.OutBack));
        hitSequence.onComplete += () => Hide();
    }
}