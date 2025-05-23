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

    public void Kill() 
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        _movementSystem.ApplyAcceleration(GetVectorToTarget());
        _movementSystem.ApplyDrag();
        _movementSystem.CollisionCheck(transform);

        transform.position += _movementSystem.State.Velocity * Time.deltaTime;

        _movementSystem.ApplyRotateToTarget(transform, _targetTransform);

        _shootingSystem.Update();
    }

    private Vector2 GetVectorToTarget()
    {
        Vector2 directionToTarget = (_targetTransform.position - transform.position).ToVector2WithXZ();
   
        var distanceToTarget = directionToTarget.magnitude;

        if (distanceToTarget < _moveAwayZoneRadius)
        {
            Vector2 directionFromTarget = (transform.position - _targetTransform.position).ToVector2WithXZ();

            return directionFromTarget.normalized;
        }

        if (distanceToTarget < _stayZoneRadius)
        {
            return Vector2.zero;
        }
        
        return directionToTarget.normalized;
    }
}
