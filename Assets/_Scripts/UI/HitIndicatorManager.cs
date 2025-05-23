using UnityEngine;

class HitIndicatorManager : MonoBehaviour
{
    [SerializeField] private HitIndicator _hitIndicatorPrefab;

    public void ShowHitIndicator(Vector3 hitDirection)
    {
        var indicator = Instantiate(_hitIndicatorPrefab, transform);
        indicator.SetHitDirection(hitDirection);
        indicator.Show();
    }
}