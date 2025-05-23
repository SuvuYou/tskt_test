using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private InputReaderSO _inputReaderSO;

    [SerializeField] private MovementStats _movementStats;
    private MovementSystem _movementSystem;

    private void Start()
    {
        _inputReaderSO.EnableActions();      

        _movementSystem = new MovementSystem(_movementStats);
    }

    private void Update()
    {
        _movementSystem.ApplyAcceleration(MapVectorToCameraSpace(_inputReaderSO.Direction));
        _movementSystem.ApplyDrag();

        transform.position += _movementSystem.State.Velocity * Time.deltaTime;
    }

    private Vector2 MapVectorToCameraSpace(Vector2 direction)
    {
        Vector3 forwardDirection = Vector3.ProjectOnPlane(_cameraTransform.forward, Vector3.up).normalized;
        
        Vector3 rightDirection = Vector3.ProjectOnPlane(_cameraTransform.right, Vector3.up).normalized;

        return (forwardDirection * direction.y + rightDirection * direction.x).ToVector2WithXZ();
    }
}
