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
        Vector2 forwardDirection = new Vector2(_cameraTransform.forward.x, _cameraTransform.forward.z).normalized;

        Vector2 rightDirection = new Vector2(_cameraTransform.right.x, _cameraTransform.right.z).normalized;

        return forwardDirection * direction.y + rightDirection * direction.x;
    }
}
