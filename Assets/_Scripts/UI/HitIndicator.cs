using System.Collections;
using UnityEngine;

class HitIndicator : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;

    public void Show() => _rectTransform.gameObject.SetActive(true);
    public void Hide() => _rectTransform.gameObject.SetActive(false);


}