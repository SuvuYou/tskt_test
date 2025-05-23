using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private MovementStats _movementStats;

    [Header("Specify zones for different disision of movement")]
    [SerializeField] private float _moveAwayZoneRadius;
    [SerializeField] private float _stayZoneRadius;

    private Transform _tragetTransform;

    private MovementSystem _movementSystem;

    public void Init(Transform target)
    {
        _tragetTransform = target;
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
    }

    private Vector2 GetVectorToTarget()
    {
        Vector2 directionToTarget = new (_tragetTransform.position.x - transform.position.x, _tragetTransform.position.z - transform.position.z);
   
        var distanceToTarget = directionToTarget.magnitude;

        if (distanceToTarget < _moveAwayZoneRadius)
        {
            Vector2 directionFromTarget = new (transform.position.x - _tragetTransform.position.x, transform.position.z - _tragetTransform.position.z);

            return directionFromTarget.normalized;
        }

        if (distanceToTarget < _stayZoneRadius)
        {
            return Vector2.zero;
        }
        
        return directionToTarget.normalized;
    }
}
