using UnityEngine;

public class MovementState
{   
    public Vector3 Velocity { get; private set; }
    public Vector3 LastNonZeroVelocity { get; private set; }

    public void SetVelocity(Vector3 velocity)
    {
        Velocity = velocity;

        if (Velocity.magnitude > 0.1f)
        {
            LastNonZeroVelocity = Velocity;
        }
    }
}