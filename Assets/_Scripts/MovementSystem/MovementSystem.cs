using Unity.VisualScripting.Antlr3.Runtime.Misc;
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

        var velocity = State.Velocity;

        velocity.x += acceleration.x;
        velocity.z += acceleration.y;

        State.SetVelocity(Vector3.ClampMagnitude(velocity, _stats.MaxVelocity));
    }   

    public void ApplyDrag()
    {
        var drag = -State.Velocity * _stats.Drag * Time.deltaTime;

        if (drag.magnitude > State.Velocity.magnitude)
        {
            State.SetVelocity(Vector3.zero);

            return;
        }

        var velocity = State.Velocity;
    
        velocity.x += drag.x;
        velocity.z += drag.z;

        State.SetVelocity(Vector3.ClampMagnitude(velocity, _stats.MaxVelocity));
    }  

    public void CollisionCheck(Transform transform)
    {
        Vector3 center = transform.position + Vector3.up * _stats.CapsuleHeight / 2;
        Vector3 dimentions = new (_stats.CapsuleRadius, _stats.CapsuleHeight / 2, _stats.CapsuleRadius);

        bool canMoveOnXAxis = !Physics.BoxCast(center, dimentions, State.Velocity.With(y: 0, z: 0).normalized, out RaycastHit hitInfoXAxis, transform.rotation, _stats.CapsuleRadius, _stats.CollisionLayer);
        bool canMoveOnZAxis = !Physics.BoxCast(center, dimentions, State.Velocity.With(y: 0, x: 0).normalized, out RaycastHit hitInfoZAxis, transform.rotation, _stats.CapsuleRadius, _stats.CollisionLayer);

        if (!canMoveOnXAxis)
        {
            State.SetVelocity(State.Velocity.With(x: 0));
        }

        if (!canMoveOnZAxis)
        {
            State.SetVelocity(State.Velocity.With(z: 0));
        }
    }

    public void ApplyRotateToTarget(Transform currentTransform, Transform target)
    {
        Vector3 direction = target.position - currentTransform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        currentTransform.rotation = Quaternion.Slerp(currentTransform.rotation, targetRotation, _stats.RotationSpeed * Time.deltaTime);
    }

    public void ApplyRotateInDirection(Transform currentTransform, Vector3 direction)
    {
        if (direction == Vector3.zero) return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        currentTransform.rotation = Quaternion.Slerp(currentTransform.rotation, targetRotation, _stats.RotationSpeed * Time.deltaTime);
    }
}