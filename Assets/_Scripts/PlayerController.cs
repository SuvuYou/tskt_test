using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private InputReaderSO _inputReaderSO;

    [SerializeField] private float _acceleration;
    [SerializeField] private float _drag;
    [SerializeField] private float _maxVelocity;

    private Vector3 _velocity;

    private void Start()
    {
        _inputReaderSO.EnableActions();        
    }

    private void Update()
    {
        ApplyAcceleration(MapVectorToCameraSpace(_inputReaderSO.Direction));
        ApplyDrag();

        transform.position += _velocity * Time.deltaTime;
    }

    private void ApplyAcceleration(Vector2 direction)
    {
        var acceleration = direction * _acceleration * Time.deltaTime;

        _velocity.x += acceleration.x;
        _velocity.z += acceleration.y;

        _velocity = Vector3.ClampMagnitude(_velocity, _maxVelocity);
    }   

    private void ApplyDrag()
    {
        var drag = -_velocity * _drag * Time.deltaTime;

        if (drag.magnitude > _velocity.magnitude)
        {
            _velocity = Vector3.zero;

            return;
        }
            
        _velocity.x += drag.x;
        _velocity.z += drag.z;

        _velocity = Vector3.ClampMagnitude(_velocity, _maxVelocity);
    }  

    private Vector2 MapVectorToCameraSpace(Vector2 direction)
    {
        Vector2 forwardDirection = new Vector2(_cameraTransform.forward.x, _cameraTransform.forward.z).normalized;

        Vector2 rightDirection = new Vector2(_cameraTransform.right.x, _cameraTransform.right.z).normalized;

        return forwardDirection * direction.y + rightDirection * direction.x;
    }
}
