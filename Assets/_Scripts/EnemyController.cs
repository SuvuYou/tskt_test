using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private MovementStats _movementStats;

    [Header("Specify zones for different disision of movement")]
    [SerializeField] private float _moveAwayZoneRadius;
    [SerializeField] private float _stayZoneRadius;

    private Transform _targetTransform;

    private MovementSystem _movementSystem;
    

    public void Init(Transform target)
    {
        _targetTransform = target;
    }

    private void Start()
    {
        _movementSystem = new MovementSystem(_movementStats);
    }

    private void Update()
    {
        _movementSystem.ApplyAcceleration(GetVectorToTarget());
        _movementSystem.ApplyDrag();

        transform.position += _movementSystem.State.Velocity * Time.deltaTime;

        _movementSystem.ApplyRotateToTarget(transform, _targetTransform);
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
