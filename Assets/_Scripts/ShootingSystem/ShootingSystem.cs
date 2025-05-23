using UnityEngine;

public class ShootingSystem
{
    private ShootingStats _stats;
    public ShootingState State;

    private Timer _shootingTimer;
    private Transform _bulletSpawnPoint;

    public ShootingSystem(ShootingStats ShootingStats, Transform bulletSpawnPoint)
    {
        _bulletSpawnPoint = bulletSpawnPoint;
        _stats = ShootingStats;

        State = new ShootingState();

        _shootingTimer = new Timer(_stats.IntervalBetweenShots);
    }

    public void SetTarget(Transform target) => State.Target = target;

    public void Enable() 
    {
        State.IsEnabled = true;
        _shootingTimer.Reset();
        _shootingTimer.Start();
    }

    public void Disable() 
    {
        State.IsEnabled = false;
        _shootingTimer.Reset();
        _shootingTimer.Stop();
    }

    public void Update() 
    {
        if (!State.IsEnabled) return;

        _shootingTimer.Update(Time.deltaTime);

        if (_shootingTimer.IsFinished) 
        {
            _shootingTimer.Reset();
            FireBullet();
        }
    }

    private void FireBullet() 
    {
        if (State.Target == null || !State.IsEnabled) return;

        Vector3 movementDirection = State.Target.position - _bulletSpawnPoint.position;
        movementDirection = Vector3.ProjectOnPlane(movementDirection, Vector3.up).normalized;

        BulletsManager.Instance.SpawnBullet(_stats, _bulletSpawnPoint, movementDirection);
    }
}