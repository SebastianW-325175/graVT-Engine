using System;
using UnityEngine;

public static class MomentOfInteriaSolver
{
    // TODO: This assumes a non-offset collider. Account for offsets using the parallel axes theorem
    public static float Solve(Collider2D collider2D, float mass)
    {
        return collider2D switch
        {
            CircleCollider2D circleCollider2D => Solve(circleCollider2D, mass),
            BoxCollider2D boxCollider2D => Solve(boxCollider2D, mass),
            _ => 1
        };
    }

    private static float Solve(CircleCollider2D circleCollider2D, float mass)
    {
        float scale = circleCollider2D.transform.lossyScale.x;
        return (float)(0.5 * mass * Math.Pow(circleCollider2D.radius * scale, 2));
    }

    private static float Solve(BoxCollider2D boxCollider2D, float mass)
    {
        float xScale = boxCollider2D.transform.lossyScale.x;
        float yScale = boxCollider2D.transform.lossyScale.y;
        return (float)(mass * (Math.Pow(boxCollider2D.size.y * yScale, 2) + Math.Pow(boxCollider2D.size.x * xScale, 2)) / 12.0);
    }
}