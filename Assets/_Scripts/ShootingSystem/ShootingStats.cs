using UnityEngine;

[CreateAssetMenu(fileName = "ShootingStats", menuName = "ScriptableObjects/ShootingStats")]
public class ShootingStats : ScriptableObject
{   
    [SerializeField] public Bullet BulletPrefab;

    [SerializeField] public float IntervalBetweenShots;
}