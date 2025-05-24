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
} 
