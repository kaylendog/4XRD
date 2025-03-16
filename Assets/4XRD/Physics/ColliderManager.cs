using System;
using System.Collections.Generic;
using _4XRD.Physics.Colliders;
using UnityEngine;

namespace _4XRD.Physics
{
    /// <summary>
    /// The scene collider manager.
    /// </summary>
    public class ColliderManager : MonoBehaviour
    {
        /// <summary>
        /// A segment of 4D space.
        /// </summary>
        struct Segment : IEquatable<Segment>
        {
            /// <summary>
            /// The size of a segment.
            /// </summary>
            public const int Size = 16;

            /// <summary>
            /// Coordinates of this segment.
            /// </summary>
            int _x, _y, _z, _w;

            /// <summary>
            /// Test whether this segment includes a given point.
            /// </summary>
            /// <param name="point"></param>
            /// <returns></returns>
            public bool Includes(Vector4 point)
            {
                var targetSegment = Of(point);
                return Equals(targetSegment);
            }

            /// <summary>
            /// Return the segment.
            /// </summary>
            /// <param name="point"></param>
            /// <returns></returns>
            public static Segment Of(Vector4 point)
            {
                return new Segment
                {
                    _x = Mathf.FloorToInt(point.x / Size),
                    _y = Mathf.FloorToInt(point.y / Size),
                    _z = Mathf.FloorToInt(point.z / Size),
                    _w = Mathf.FloorToInt(point.w / Size)
                };
            }
            
            public bool Equals(Segment other)
            {
                return _x == other._x && _y == other._y && _z == other._z && _w == other._w;
            }

            public override bool Equals(object obj)
            {
                return obj is Segment other && Equals(other);
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(_x, _y, _z, _w);
            }
        }
        
        /// <summary>
        /// A list of all colliders.
        /// </summary>
        StaticCollider4D[] _colliders;

        readonly Dictionary<Segment, HashSet<StaticCollider4D>> _segmentToColliders = new();
        readonly Dictionary<StaticCollider4D, Segment> _colliderToSegment = new();

        void Awake()
        {
            _colliders = FindObjectsByType<StaticCollider4D>(FindObjectsSortMode.None);
        }

        void FixedUpdate()
        {
            UpdateSegments();
            ResolveCollisions();
        }

        /// <summary>
        /// Update the segments each collider belongs to.
        /// </summary>
        void UpdateSegments()
        {
            foreach (var col in _colliders)
            {
                var segment = Segment.Of(col.transform4D.position);
    
                // create set if it does not exist
                if (!_segmentToColliders.ContainsKey(segment))
                {
                    _segmentToColliders[segment] = new HashSet<StaticCollider4D>();
                }
                
                // update forward edges
                _segmentToColliders[_colliderToSegment[col]].Remove(col);
                _segmentToColliders[segment].Add(col);
                
                // store backwards edge
                _colliderToSegment[col] = segment;
            }
        }

        /// <summary>
        /// Resolve collisions.
        /// </summary>
        void ResolveCollisions()
        {
            
        }
    }
}
