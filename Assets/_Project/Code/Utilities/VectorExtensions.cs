#nullable enable
using UnityEngine;

public static class VectorExtensions
{
    public static Vector3 WithZ(this Vector2 vector2, int z)
    {
        return new Vector3(vector2.x, vector2.y, z);
    }
}