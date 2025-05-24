using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    [SerializeField] private Transform _resetPositionTransform;
    [SerializeField] private Vector3 _resetPosition;

    private void Update()
    {
        _resetPositionTransform.localPosition = _resetPosition;
        _resetPositionTransform.localRotation = Quaternion.identity;
    } 
}
