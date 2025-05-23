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
}