using UnityEngine;

[CreateAssetMenu(fileName = "MovementStats", menuName = "ScriptableObjects/MovementStats")]
public class MovementStats : ScriptableObject
{   
    [Header("Movement")]
    public float Acceleration;
    public float Drag;
    public float MaxVelocity;

    [Header("Rotation")]
    public float RotationSpeed;

    [Header("Collision")]
    public float CapsuleHeight = 2.0f;
    public float CapsuleRadius = 0.5f;
    public LayerMask CollisionLayer;

    [Header("Animation")]
    public float VelocityMovementThreshold = 8f;
}