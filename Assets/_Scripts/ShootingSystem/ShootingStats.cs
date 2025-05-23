using UnityEngine;

[CreateAssetMenu(fileName = "ShootingStats", menuName = "ScriptableObjects/ShootingStats")]
public class ShootingStats : ScriptableObject
{   
    public Bullet BulletPrefab;

    public float IntervalBetweenShots;
}