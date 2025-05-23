using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private MovementStats _movementStats;
    [SerializeField] private ShootingStats _shootingStats;

    [SerializeField] private Transform _bulletSpawnPoint;

    [Header("Specify zones for different disision of movement")]
    [SerializeField] private float _moveAwayZoneRadius;
    [SerializeField] private float _stayZoneRadius;

    private Transform _targetTransform;

    private MovementSystem _movementSystem;
    private ShootingSystem _shootingSystem;
    
    public void Init(Transform target)
    {
        _movementSystem = new MovementSystem(_movementStats);
        _shootingSystem = new ShootingSystem(_shootingStats, _bulletSpawnPoint);

        _targetTransform = target;

        _shootingSystem.SetTarget(target);
        _shootingSystem.Enable();
    }

    private void Update()
    {
        _movementSystem.ApplyAcceleration(GetVectorToTarget());
        _movementSystem.ApplyDrag();

        transform.position += _movementSystem.State.Velocity * Time.deltaTime;

        _movementSystem.ApplyRotateToTarget(transform, _targetTransform);

        _shootingSystem.Update();
    }

    private Vector2 GetVectorToTarget()
    {
        Vector2 directionToTarget = new (_targetTransform.position.x - transform.position.x, _targetTransform.position.z - transform.position.z);
   
        var distanceToTarget = directionToTarget.magnitude;

        if (distanceToTarget < _moveAwayZoneRadius)
        {
            Vector2 directionFromTarget = new (transform.position.x - _targetTransform.position.x, transform.position.z - _targetTransform.position.z);

            return directionFromTarget.normalized;
        }

        if (distanceToTarget < _stayZoneRadius)
        {
            return Vector2.zero;
        }
        
        return directionToTarget.normalized;
    }
}
