using UnityEngine;

[CreateAssetMenu(fileName = "MovementStats", menuName = "ScriptableObjects/MovementStats")]
public class MovementStats : ScriptableObject
{   
    [Header("Movement")]
    [SerializeField] public float Acceleration;
    [SerializeField] public float Drag;
    [SerializeField] public float MaxVelocity;

    [Header("Rotation")]
    [SerializeField] public float RotationSpeed;
}