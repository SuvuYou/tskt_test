using UnityEngine;

[CreateAssetMenu(fileName = "MovementStats", menuName = "ScriptableObjects/MovementStats")]
public class MovementStats : ScriptableObject
{   
    [SerializeField] public float Acceleration;
    [SerializeField] public float Drag;
    [SerializeField] public float MaxVelocity;
}