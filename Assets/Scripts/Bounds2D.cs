using UnityEngine;

namespace DefaultNamespace
{
    public struct Bounds2D
    {
        public Vector2 center;
        public Vector2 size;

        #region TODO: Test this later cause I feel like this doesn't work 
        public Vector2 extents
        {
            readonly get => size * 0.5f;
            set => this.size = value * 0.5f;
        }

        public Vector2 max
        {
            readonly get => center + extents;
            set => this.size = (value - center) * 0.5f;
        }

        public Vector2 min
        {
            readonly get => center - extents;
            set => this.size = (value + center) * 0.5f;
        }
        #endregion

        public Bounds2D(Vector2 center, Vector2 size)
        {
            this.center = center;
            this.size = size;
        }

        public readonly bool Contains(Bounds2D bounds)
        {
            return (
                min.x <= bounds.min.x &&
                min.y <= bounds.min.y &&
                max.x > bounds.max.x &&
                max.y > bounds.max.y
            );
        }
        
        public readonly bool Intersects(Bounds2D bounds)
        {
            return (
                min.x < bounds.max.x &&
                max.x > bounds.min.x &&
                min.y < bounds.max.y &&
                max.y > bounds.min.y
            );
        }
        
        //TODO: Implement all the methods of the base Unity Bounds class
    }
}