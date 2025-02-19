using UnityEngine;

namespace _4XRD.Physics
{
    /// <summary>
    /// A 4-dimensional version of unity's built-in Euler.
    /// </summary>
    public struct Euler4
    {
        public float XY; // Z (W)
        public float YZ; // X (w)
        public float XZ; // Y (W)
        public float XW; // Y Z
        public float YW; // X Z
        public float ZW; // X Y
    
        /// <summary>
        /// A zero-initialised instance of Euler4.
        /// </summary>
        public static Euler4 Zero
        {
            get => new();
        }
    }
}
