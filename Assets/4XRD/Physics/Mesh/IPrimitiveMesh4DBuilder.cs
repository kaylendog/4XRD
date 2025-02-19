namespace _4XRD.Physics.Mesh
{
    /// <summary>
    ///     An interface representing types capable of building 4D meshes.
    /// </summary>
    public interface IPrimitiveMesh4DBuilder
    {
        /// <summary>
        ///     Build the mesh.
        /// </summary>
        /// <returns></returns>
        public Mesh4D Build();
    }
}
