using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace DefaultNamespace
{
    //TODO: Edge cases to handle:
    // 1. A body occupies multiple quadrants
    // 2. A body lays perfectly on a boundary
    // 3. What about size? Attempting insertion of a body larger than the quadrant causes infinite subdivision
    // 4. Max depth? What if there's many small objects in the same area?
    // 5. Identical bounds
    public class QuadTree
    {
        #nullable enable
        private QuadTree? _tlQuadrant;
        private QuadTree? _trQuadrant;
        private QuadTree? _blQuadrant;
        private QuadTree? _brQuadrant;
        private List<IBoundingBox> _values;
        #nullable disable
        private Bounds2D _bounds;

        public QuadTree()
        {
            _bounds = new Bounds2D(
                Vector2.zero,
                new Vector2(
                    float.MaxValue,
                    float.MaxValue
                )
            );
        }
        public QuadTree(Bounds2D bounds)
        {
            _bounds = bounds;
        }
        
        public bool Contains(Bounds2D bounds)
        {
            return _bounds.Contains(bounds);
        }
        
        public void Insert(IBoundingBox value)
        {
            Bounds2D bounds = value.GetBounds();
            if (!Contains(bounds)) throw new Exception("Root tree too small to fit object.");
            QuadTree[] containingQuadrants = GetContainingQuadrants(bounds);

            if (containingQuadrants.Length == 1) containingQuadrants[0].Insert(value);
            else _values.Add(value);
        }

        private bool isPopulated()
        {
            return _tlQuadrant != null && _trQuadrant != null && _blQuadrant != null && _brQuadrant != null;
        }

        private void Populate()
        {
            _tlQuadrant = new QuadTree(new Bounds2D( _bounds.center + _bounds.extents * -0.5f, _bounds.extents * 0.5f ));
            _trQuadrant = new QuadTree(new Bounds2D( _bounds.center + _bounds.extents * new Vector2(0.5f, -0.5f), _bounds.extents * 0.5f));
            _blQuadrant = new QuadTree(new Bounds2D( _bounds.center + _bounds.extents * new Vector2(-0.5f, 0.5f),  _bounds.extents * 0.5f));
            _brQuadrant = new QuadTree(new Bounds2D(_bounds.center + _bounds.extents * 0.5f, _bounds.extents * 0.5f));
        }
        
        private QuadTree[] GetContainingQuadrants(Bounds2D bounds)
        {
            if (!isPopulated()) Populate();
            List<QuadTree> result = new List<QuadTree>();
            // Quadrants can no longer be null, because they were checked and Populated
            if(_tlQuadrant!.Contains(bounds)) result.Add(_tlQuadrant);
            if(_trQuadrant!.Contains(bounds)) result.Add(_trQuadrant);
            if(_blQuadrant!.Contains(bounds)) result.Add(_blQuadrant);
            if(_brQuadrant!.Contains(bounds)) result.Add(_brQuadrant);
            
            return result.ToArray();
        }
    }
}