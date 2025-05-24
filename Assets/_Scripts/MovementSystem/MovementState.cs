using System;
using UnityEngine;

public class MovementState
{   
    public event Action OnStartMoving = delegate { };
    public event Action OnStopMoving = delegate { };

    public Vector3 Velocity { get; private set; }
    public Vector3 LastNonZeroVelocity { get; private set; }

    private float _velocityMovementThreshold;

    public void SetVelocityThreshold(float threshold) => _velocityMovementThreshold = threshold;

    public void SetVelocity(Vector3 velocity)
    {
        Velocity = velocity;

        if (Velocity.magnitude > 0.1f)
        {
            LastNonZeroVelocity = Velocity;
        }

        if (Velocity.magnitude > _velocityMovementThreshold) OnStartMoving?.Invoke();
        
        if (Velocity.magnitude <= _velocityMovementThreshold) OnStopMoving?.Invoke();
    }
}