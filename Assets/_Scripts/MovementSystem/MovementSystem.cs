using UnityEngine;

public class MovementSystem
{
    private MovementStats _stats;
    public MovementState State;

    public MovementSystem(MovementStats movementStats)
    {
        _stats = movementStats;

        State = new MovementState();
    }

    public void ApplyAcceleration(Vector2 direction)
    {
        var acceleration = direction * _stats.Acceleration * Time.deltaTime;

        State.Velocity.x += acceleration.x;
        State.Velocity.z += acceleration.y;

        State.Velocity = Vector3.ClampMagnitude(State.Velocity, _stats.MaxVelocity);
    }   

    public void ApplyDrag()
    {
        var drag = -State.Velocity * _stats.Drag * Time.deltaTime;

        if (drag.magnitude > State.Velocity.magnitude)
        {
            State.Velocity = Vector3.zero;

            return;
        }
            
        State.Velocity.x += drag.x;
        State.Velocity.z += drag.z;

        State.Velocity = Vector3.ClampMagnitude(State.Velocity, _stats.MaxVelocity);
    }  

    public void ApplyRotateToTarget(Transform currentTransform, Transform target)
    {
        Vector3 direction = target.position - currentTransform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        currentTransform.rotation = Quaternion.Slerp(currentTransform.rotation, targetRotation, _stats.RotationSpeed * Time.deltaTime);
    }
}