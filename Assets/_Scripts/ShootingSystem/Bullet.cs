using UnityEngine;

public class Bullet : MonoBehaviour
{   
    [SerializeField] private float _speed;

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
        BulletsManager.Instance.DestroyBullet(this);
    }
}
