using UnityEngine;

public class Bullet : MonoBehaviour
{   
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;

    private Vector3 _movementDirection;

    public void Init(Vector3 movementDirection)
    {
        _movementDirection = movementDirection;
    }

    private void Update()
    {
        transform.position += _movementDirection * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.TryGetComponent(out IDamagable damagable))
        {
            var globalDirectionFromTarget = transform.position - other.transform.root.position;
            var localDirectionFromTarget = other.transform.root.transform.InverseTransformDirection(globalDirectionFromTarget);

            Vector2 hitDirection = Vector3.ProjectOnPlane(localDirectionFromTarget, Vector3.up).normalized.ToVector2WithXZ();

            damagable.TakeDamage(_damage, hitDirection);
        }

        BulletsManager.Instance.DestroyBullet(this);
    }
}
