using System.Collections.Generic;
using UnityEngine;

public class BulletsManager : Singleton<BulletsManager>
{   
    [SerializeField] private Transform _parent;

    private List<Bullet> _bullets = new ();

    public void SpawnBullet(ShootingStats _stats, Transform _bulletSpawnPoint, Vector3 movementDirection) 
    {
        var bullet = Instantiate(_stats.BulletPrefab, _bulletSpawnPoint.position, Quaternion.identity, _parent);
        bullet.Init(movementDirection);

        _bullets.Add(bullet);
    } 

    public void DestroyBullet(Bullet bullet) 
    {
        _bullets.Remove(bullet);
        
        Destroy(bullet.gameObject);
    }
}
