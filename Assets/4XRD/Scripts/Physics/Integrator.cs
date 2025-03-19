#nullable enable
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

using _4XRD.Transform;
using _4XRD.Physics.Colliders;
using _4XRD.XR;

namespace _4XRD.Physics
{
    /// <summary>
    /// The scene collider manager.
    /// </summary>
    public class Integrator : MonoBehaviour
    {
        // /// <summary>
        // /// A segment of 4D space.
        // /// </summary>
        // struct Segment : IEquatable<Segment>
        // {
        //     /// <summary>
        //     /// The size of a segment.
        //     /// </summary>
        //     public const int Size = 16;

        //     /// <summary>
        //     /// Coordinates of this segment.
        //     /// </summary>
        //     int _x, _y, _z, _w;

        //     /// <summary>
        //     /// Test whether this segment includes a given point.
        //     /// </summary>
        //     /// <param name="point"></param>
        //     /// <returns></returns>
        //     public bool Includes(Vector4 point)
        //     {
        //         var targetSegment = Of(point);
        //         return Equals(targetSegment);
        //     }

        //     /// <summary>
        //     /// Return the segment.
        //     /// </summary>
        //     /// <param name="point"></param>
        //     /// <returns></returns>
        //     public static Segment Of(Vector4 point)
        //     {
        //         return new Segment
        //         {
        //             _x = Mathf.FloorToInt(point.x / Size),
        //             _y = Mathf.FloorToInt(point.y / Size),
        //             _z = Mathf.FloorToInt(point.z / Size),
        //             _w = Mathf.FloorToInt(point.w / Size)
        //         };
        //     }

        //     public bool Equals(Segment other)
        //     {
        //         return _x == other._x && _y == other._y && _z == other._z && _w == other._w;
        //     }

        //     public override bool Equals(object obj)
        //     {
        //         return obj is Segment other && Equals(other);
        //     }

        //     public override int GetHashCode()
        //     {
        //         return HashCode.Combine(_x, _y, _z, _w);
        //     }
        // }

        // readonly Dictionary<Segment, HashSet<StaticCollider4D>> _segmentToColliders = new();
        // readonly Dictionary<StaticCollider4D, Segment> _colliderToSegment = new();

        static ProfilerMarker integrateColliders = new ProfilerMarker("IntegrateColliders");
        static ProfilerMarker integrateBalls = new ProfilerMarker("IntegrateBalls");

        static ProfilerMarker checkCollisions = new ProfilerMarker("CheckCollisions");
        static ProfilerMarker resolveCollisions = new ProfilerMarker("ResolveCollisions");

        [SerializeField]
        ARPlane4DController? arPlane4DController;

        /// <summary>
        /// A list of all colliders.
        /// </summary>
        [SerializeField]
        List<StaticCollider4D> _colliders = new();

        /// <summary>
        /// A list of all balls.
        /// </summary>
        [SerializeField]
        List<Ball4D> _balls = new();

        /// <summary>
        /// Number of substeps per update.
        /// </summary>
        public int substeps = 8;

        /// <summary>
        /// Bias to add to normal offsets.
        /// </summary>
        public float bias = 0.05f;

        /// <summary>
        /// Length of the void before ball destruction.
        /// </summary>
        public float voidPadding = 1;
        
        void FixedUpdate()
        {
            // UpdateSegments();
            UpdateIntegrands();
            for (int i = 0; i < substeps; ++i)
            {
                IntegrateVelocities();
                IntegrateCollisions();
            }
        }

        // /// <summary>
        // /// Update the segments each collider belongs to.
        // /// </summary>
        // void UpdateSegments()
        // {
        //     foreach (var col in _colliders)
        //     {
        //         var segment = Segment.Of(col.transform4D.position);

        //         // create set if it does not exist
        //         if (!_segmentToColliders.ContainsKey(segment))
        //         {
        //             _segmentToColliders[segment] = new HashSet<StaticCollider4D>();
        //         }

        //         // update forward edges
        //         _segmentToColliders[_colliderToSegment[col]].Remove(col);
        //         _segmentToColliders[segment].Add(col);

        //         // store backwards edge
        //         _colliderToSegment[col] = segment;
        //     }
        // }

        /// <summary>
        /// Update the integrands.
        /// </summary>
        void UpdateIntegrands()
        {
            _colliders = new();
            _balls = new();
            foreach (var col in FindObjectsByType<StaticCollider4D>(FindObjectsSortMode.None))
            {
                _colliders.Add(col);
            }
            foreach (var ball in FindObjectsByType<Ball4D>(FindObjectsSortMode.None))
            {
                _balls.Add(ball);
            }
        }

        void IntegrateVelocities()
        {
            foreach (var current in _balls)
            {
                if (current.object4D.isStatic)
                {
                    continue;
                }

                Transform4D currentTransform = current.object4D.transform4D;
                
                // apply gravity
                current.velocity += new Vector4(0, -9.8f, 0, 0) * Time.fixedDeltaTime / substeps;

                // apply velocity
                current.object4D.SetPosition(
                    currentTransform.position + current.velocity * Time.fixedDeltaTime / substeps
                );

                if (arPlane4DController != null) {
                    if (currentTransform.position.y < arPlane4DController.minY - voidPadding)
                    {
                        Destroy(gameObject);
                    }
                }
                
            }
        }

        /// <summary>
        /// Resolve collisions. Currently ignores segments for speed of implementation.
        /// </summary>
        void IntegrateCollisions()
        {
            foreach (var current in _balls)
            {
                // ignore static bodies
                if (current.object4D.isStatic)
                {
                    continue;
                }

                integrateBalls.Begin();
                foreach (var other in _balls)
                {
                    if (current == other)
                    {
                        continue;
                    }

                    // check if overlapping - line of centers (loc)
                    var loc = other.transform4D.position - current.transform4D.position;
                    if (loc.magnitude > current.radius + other.radius + bias)
                    {
                        continue;
                    }

                    // move balls apart
                    var locNorm = loc.normalized;
                    var escapeVector = (current.radius + other.radius - loc.magnitude) * locNorm;
                    current.object4D.SetPosition(
                        current.transform4D.position - escapeVector / 2f
                    );
                    other.object4D.SetPosition(
                        other.transform4D.position + escapeVector / 2f
                    );

                    // compute outgoing velocities (elastic)
                    var outCurrSpeed = ((current.mass - other.mass) * current.velocity.Dot(locNorm) + 2f * other.mass * other.velocity.Dot(locNorm)) / (current.mass + other.mass);
                    var outOtherSpeed = ((other.mass - current.mass) * other.velocity.Dot(locNorm) + 2f * current.mass * current.velocity.Dot(locNorm)) / (current.mass + other.mass);

                    current.velocity = outCurrSpeed * locNorm + current.velocity - current.velocity.Dot(locNorm) * locNorm;
                    other.velocity = outOtherSpeed * locNorm + other.velocity - other.velocity.Dot(locNorm) * locNorm;
                }
                integrateBalls.End();

                integrateColliders.Begin();
                foreach (var col in _colliders)
                {
                    checkCollisions.Begin();

                    var normal = col.Normal(current.transform4D.position);
                    var surfacePoint = col.ClosestPoint(current.transform4D.position) + bias * normal;
                    var d = current.transform4D.position - surfacePoint;

                    // Debug.Log($"ball: {current.gameObject.name}, col: {col.gameObject.name}, normal: {normal}, d: {d}, dot: {d.Dot(normal)}");

                    // for convex objects, we are always inside if we lie behind the plane defined by the normal
                    if (d.Dot(normal) > current.radius)
                    {
                        // checkCollisions.End();
                        continue;
                    }

                    checkCollisions.End();
                    resolveCollisions.Begin();

                    // move away from surface
                    current.object4D.SetPosition(surfacePoint + (normal * current.radius));

                    // compute outgoing velocity (inelastic)
                    var normalVelocity = current.velocity.Dot(normal) * normal;
                    var tangentVelocity = current.velocity - normalVelocity;

                    // Debug.Log(
                    //     $"Collision: {current.gameObject.name} with {col.gameObject.name} Incoming velocity: {current.velocity}, Outgoing Velocity: {tangentVelocity * col.friction - normalVelocity * col.restitution}"
                    // );

                    current.velocity = tangentVelocity * col.friction - normalVelocity * col.restitution;

                    resolveCollisions.End();
                }
                integrateColliders.End();
            }
        }
    }
}
