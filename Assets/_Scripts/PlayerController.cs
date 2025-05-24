using UnityEngine;

public class PlayerController : MonoBehaviour, IDamagable
{
    public HitEvent OnTakeDamage;

    public void TakeDamage(int damage, Vector3 hitDirection) 
    {
        _healthSystem.TakeDamage(damage);

        OnTakeDamage?.Invoke(hitDirection);
    } 

    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private Transform _playerMesh;
    [SerializeField] private InputReaderSO _inputReaderSO;

    [SerializeField] private MovementStats _movementStats;
    [SerializeField] private HealthStats _healthStats;

    public MovementSystem PlayerMovementSystem { get; private set; }
    private HealthSystem _healthSystem;

    private void Awake()
    {
        _inputReaderSO.EnableActions();      

        PlayerMovementSystem = new MovementSystem(_movementStats);
        _healthSystem = new HealthSystem(_healthStats);
    }

    private void Update()
    {
        PlayerMovementSystem.ApplyAcceleration(MapVectorToCameraSpace(_inputReaderSO.Direction));
        PlayerMovementSystem.ApplyDrag();
        PlayerMovementSystem.CollisionCheck(transform);

        transform.position += PlayerMovementSystem.State.Velocity * Time.deltaTime;

        PlayerMovementSystem.ApplyRotateInDirection(_playerMesh, GetFacingDirection());
    }

    private Vector2 MapVectorToCameraSpace(Vector2 direction)
    {
        Vector3 forwardDirection = Vector3.ProjectOnPlane(_cameraTransform.forward, Vector3.up).normalized;
        
        Vector3 rightDirection = Vector3.ProjectOnPlane(_cameraTransform.right, Vector3.up).normalized;

        return (forwardDirection * direction.y + rightDirection * direction.x).ToVector2WithXZ();
    }

    private Vector3 GetFacingDirection()
    {
        return PlayerMovementSystem.State.LastNonZeroVelocity.normalized;
    }
}
