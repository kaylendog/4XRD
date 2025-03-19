using Unity.Profiling;
using UnityEngine;

using _4XRD.Transform;

namespace _4XRD.Physics.Colliders
{
    /// <summary>
    /// An arbitrary 4D collider.
    /// </summary>
    [RequireComponent(typeof(Object4D))]
    public abstract class StaticCollider4D : MonoBehaviour
    {
        /// <summary>
        /// Internal reference to the 4D object.
        /// </summary>
        protected Object4D object4D { get; private set; }

        /// <summary>
        /// Internal reference to the 4D transform.
        /// </summary>
        public Transform4D transform4D => object4D.transform4D;

        /// <summary>
        /// The restitution of this surface.
        /// </summary>
        public float restitution = 0.6f;

        /// <summary>
        /// The friction of this surface.
        /// </summary>
        public float friction = 0.95f;

        protected virtual void Awake()
        {
            object4D = GetComponent<Object4D>();
        }

        // /// <summary>
        // /// Find the closest point to the collider, in local space.
        // /// </summary>
        // /// <param name="position"></param>
        // /// <returns></returns>
        // protected abstract Vector4 LocalClosestPoint(Vector4 position);

        /// <summary>
        /// Find the closest point to the collider surface, in world space.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public abstract Vector4 ClosestPoint(Vector4 position);

        // /// <summary>
        // /// Return the closet point on this colliders surface.
        // /// </summary>
        // /// <param name="position"></param>
        // /// <param name="radius"></param>
        // /// <returns></returns>
        // public Vector4 IterativeClosestPoint(Vector4 position, float radius)
        // {
        //     markerClosestPoint.Begin();
        //     var (theta0, theta1, theta2, theta3) = MathNet.Numerics.FindMinimum.OfFunction(
        //         (theta0, theta1, theta2, theta3) => (LocalClosestPoint(ComputeBoundaryPoint(radius, (float)theta0, (float)theta1, (float)theta2, (float)theta3)) - position).magnitude,
        //         0.0,
        //         0.0,
        //         0.0,
        //         0.0,
        //         maxIterations: 4
        //     );
        //     markerClosestPoint.End();
        //     return transform4D.inverse * LocalClosestPoint(ComputeBoundaryPoint(radius, (float)theta0, (float)theta1, (float)theta2, (float)theta3));
        // }

        // Vector4 ComputeBoundaryPoint(float radius, float theta0, float theta1, float theta2, float theta3)
        // {
        //     var sinTheta0 = Mathf.Sin(theta0);
        //     var sinTheta1 = Mathf.Cos(theta0);
        //     var sinTheta2 = Mathf.Sin(theta0);

        //     return transform4D.apply(new Vector4(
        //         radius * Mathf.Cos(theta0),
        //         radius * sinTheta0 * Mathf.Cos(theta1),
        //         radius * sinTheta0 * sinTheta1 * Mathf.Cos(theta2),
        //         radius * sinTheta0 * sinTheta1 * sinTheta2 * Mathf.Cos(theta3)
        //     ));
        // }

        // /// <summary>
        // /// The normal to the surface at a given point, in local space.
        // /// </summary>
        // /// <param name="position"></param>
        // /// <returns></returns>
        // protected abstract Vector4 LocalNormal(Vector4 position);

        /// <summary>
        /// The normal to the surface at a given point, in world space.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public abstract Vector4 Normal(Vector4 position);
    }
}
