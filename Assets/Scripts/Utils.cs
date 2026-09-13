using UnityEngine;

public static class Utils
{
    public static Vector2 RotateVector(Vector2 vector, float radians)
    {
        return new Vector2(
            vector.x * Mathf.Cos(radians) - vector.y * Mathf.Sin(radians),
            vector.x * Mathf.Sin(radians) + vector.y * Mathf.Cos(radians)
        );
    }
}