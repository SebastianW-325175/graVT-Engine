using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.Android;

namespace GraVTEngine
{
    public struct Bounds2D
    {
        private Vector2 _center;
        private Vector2 _extents;

        public Vector2 center
        {
            readonly get => this.center;
            set => _center = value;
        }

        public Vector2 size
        {
            readonly get
            {
                return new Vector2()
                {
                    x = _extents.x * 2.0f,
                    y = _extents.y * 2.0f
                };
            }
            set
            {
                _extents.x = value.x * 0.5f;
                _extents.y = value.y * 0.5f;
            }
        }

        public Vector2 extents
        {
            readonly get => _extents;
            set => _extents = value;
        }

        public Vector2 min
        {
            readonly get
            {
                return new Vector2()
                {
                    x = _center.x - _extents.x,
                    y = _center.y - _extents.y
                };
            }
            set => this.SetMinMax(in value, this.max);
        }

        public Vector2 max
        {
            readonly get
            {
                return new Vector2()
                {
                    x = _center.x + _extents.x,
                    y = _center.y + _extents.y
                };
            }
            set => this.SetMinMax(this.min, in value);
        }

        public static bool operator ==(Bounds2D lhs, Bounds2D rhs)
        {
            return lhs._center == rhs._center && lhs._extents == rhs._extents;
        }

        public static bool operator !=(Bounds2D lhs, Bounds2D rhs)
        {
            return !(lhs._center == rhs._center) || !(lhs._extents == rhs._extents);
        }

        public Bounds2D(Vector2 center, Vector2 size)
        {
            _center.x = center.x;
            _center.y = center.y;
            _extents.x = size.x * 0.5f;
            _extents.y = size.y * 0.5f;
        }

        public Bounds2D(in Vector2 center, in Vector2 size)
        {
            _center.x = center.x;
            _center.y = center.y;
            _extents.x = size.x * 0.5f;
            _extents.y = size.y * 0.5f;
        }

        public void SetMinMax(Vector2 min, Vector2 max)
        {
            _extents.x = (float) (((double) max.x - (double) min.x) * 0.5f);
            _extents.y = (float) (((double) max.y - (double) min.y) * 0.5f);
            _center.x = min.x + _extents.x;
            _center.y = min.y + _extents.y;
        }
        
        public void SetMinMax(in Vector2 min, in Vector2 max)
        {
            _extents.x = (float) (((double) max.x - (double) min.x) * 0.5f);
            _extents.y = (float) (((double) max.y - (double) min.y) * 0.5f);
            _center.x = min.x + _extents.x;
            _center.y = min.y + _extents.y;
        }

        public override readonly int GetHashCode()
        {
            return _center.GetHashCode() ^ _extents.GetHashCode() << 2;
        }

        public override readonly bool Equals(object other)
        {
            return other is Bounds2D other1 && Equals(in other1);
        }

        public readonly bool Equals(Bounds2D other)
        {
            return _center.Equals(in other._center) && _extents.Equals(in other._extents);
        }

        public readonly bool Equals(in Bounds2D other)
        {
            return _center.Equals(in other._center) && _extents.Equals(in other._extents);
        }

        public void Encapsulate(Vector2 point)
        {
            Vector2 min = Vector2.Min(this.min, in point);
            Vector2 max = Vector2.Max(this.max, in point);
            SetMinMax(in min, in max);
        }

        public void Encapsulate(in Vector2 point)
        {
            Vector2 min = Vector2.Min(this.min, in point);
            Vector2 max = Vector2.Max(this.max, in point);
            SetMinMax(in min, in max);
        }

        public void Encapsulate(Bounds2D bounds)
        {
            Vector2 point1 = bounds.min;
            Vector2 point2 = bounds.max;
            Encapsulate(in point1);
            Encapsulate(in point2);
        }

        public void Encapsulate(in Bounds bounds)
        {
            Vector2 points1 = bounds.min;
            Vector2 points2 = bounds.max;
            Encapsulate(in points1);
            Encapsulate(in points2);
        }

        public void Expand(float amount)
        {
            amount *= 0.5f;
            _extents.x += amount;
            _extents.y += amount;
        }

        public void Expand(Vector2 amount)
        {
            _extents.x += amount.x * 0.5f;
            _extents.y += amount.y * 0.5f;
        }

        public void Expand(in Vector2 amount)
        {
            _extents.x += amount.x * 0.5f;
            _extents.y += amount.y * 0.5f;
        }

        public readonly bool Intersects(Bounds2D bounds)
        {
            Vector2 min1 = this.min;
            Vector2 max1 = this.max;
            Vector2 min2 = bounds.min;
            Vector2 max2 = bounds.max;
            return (double) min1.x <= (double) max2.x && (double) max1.x >= (double) min2.x && (double) min1.y <= (double) max2.y && (double) max1.y >= (double) min2.y;
        }
        
        public readonly bool Intersects(in Bounds2D bounds)
        {
            Vector2 min1 = this.min;
            Vector2 max1 = this.max;
            Vector2 min2 = bounds.min;
            Vector2 max2 = bounds.max;
            return (double) min1.x <= (double) max2.x && (double) max1.x >= (double) min2.x && (double) min1.y <= (double) max2.y && (double) max1.y >= (double) min2.y;
        }

        public override readonly string ToString()
        {
            return this.ToString((string)null, (IFormatProvider)null);
        }

        public readonly string ToString(string format) => this.ToString(format, (IFormatProvider)null);

        public readonly string ToString(string format, IFormatProvider formatProvider)
        {
            if (string.IsNullOrEmpty(format))
                format = "F2";
            if (formatProvider == null)
                formatProvider = (IFormatProvider) CultureInfo.InvariantCulture.NumberFormat;
            return $"Center: {_center.ToString(format, formatProvider)}, Extents: {_extents.ToString(format, formatProvider)}";
        }

        public readonly bool Contains(Vector2 point)
        {
            return (
                min.x <= point.x &&
                min.y <= point.y &&
                max.x >= point.x &&
                max.y >= point.y
            );
        }
        
        public readonly bool Contains(in Vector2 point)
        {
            return (
                min.x <= point.x &&
                min.y <= point.y &&
                max.x >= point.x &&
                max.y >= point.y
            );
        }

        public readonly Vector2 ClosestPoint(Vector2 point)
        {
            if (Contains(point)) return point;
            return new Vector2( Mathf.Clamp(point.x, min.x, max.x), Mathf.Clamp(point.y, min.y, max.y) );
        }

        public readonly float SqrDistance(Vector2 point)
        {
            if (Contains(point)) return 0.0f;
            Vector2 closestPoint = ClosestPoint(point);
            return Mathf.Pow((point.x - closestPoint.x), 2) + Mathf.Pow((point.y - closestPoint.y), 2);
        }
    }
}