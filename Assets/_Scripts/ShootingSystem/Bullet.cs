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
            Vector2 hitDirection = Vector3.ProjectOnPlane(transform.position - other.transform.position, Vector3.up).normalized.ToVector2WithXZ();

            damagable.TakeDamage(_damage, hitDirection.ClosestHitDirection());
        }

        BulletsManager.Instance.DestroyBullet(this);
    }
}
