using UnityEngine;

class HitIndicatorManager : MonoBehaviour
{
    [SerializeField] private HitIndicator _hitIndicatorLeft, _hitIndicatorRight, _hitIndicatorTop, _hitIndicatorBottom;

    public void ShowHitIndicator(HitDirection hitDirection, int damage)
    {
        _hitIndicatorLeft.Hide();
        _hitIndicatorRight.Hide();
        _hitIndicatorTop.Hide();
        _hitIndicatorBottom.Hide();

        switch (hitDirection)
        {
            case HitDirection.Left: _hitIndicatorLeft.Show(); break;
            case HitDirection.Right: _hitIndicatorRight.Show(); break;
            case HitDirection.Forward: _hitIndicatorTop.Show(); break;
            case HitDirection.Backward: _hitIndicatorBottom.Show(); break;
        }
    }
}