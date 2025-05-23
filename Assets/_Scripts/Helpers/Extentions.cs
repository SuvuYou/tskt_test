using System.Collections.Generic;
using UnityEngine;

public static class VectorExtentions
{
    public static Vector3 With(this Vector3 vector, float? x = null, float? y = null, float? z = null)
    {
        vector.x = x ?? vector.x;
        vector.y = y ?? vector.y;
        vector.z = z ?? vector.z;

        return vector;
    }

    public static Vector2 With(this Vector2 vector, float? x = null, float? y = null)
    {
        vector.x = x ?? vector.x;
        vector.y = y ?? vector.y;

        return vector;
    }

    public static Vector2 ToVector2WithXZ(this Vector3 vector) => new (vector.x, vector.z);

    public static HitDirection ClosestHitDirection(this Vector2 vector)
    {
        float minDistance = float.MaxValue;
        HitDirection closestHitDirection = HitDirection.Forward;

        foreach (HitDirection hitDirection in System.Enum.GetValues(typeof(HitDirection)))
        {
            Vector2 hitDirectionVector = hitDirection.ToVector2();
            float distance = Vector2.Distance(hitDirectionVector, vector);
            
            if (distance < minDistance)
            {
                minDistance = distance;
                closestHitDirection = hitDirection;
            }
        }

        return closestHitDirection;
    }
} 

public enum HitDirection { Forward, Backward, Left, Right }

public static class HitDirectionExtentions
{
    public static Vector2 ToVector2(this HitDirection hitDirection) => hitDirection switch
    {
        HitDirection.Forward => Vector2.down,
        HitDirection.Backward => Vector2.up,
        HitDirection.Left => Vector2.right,
        HitDirection.Right => Vector2.left,
        _ => Vector2.zero
    };

    
}