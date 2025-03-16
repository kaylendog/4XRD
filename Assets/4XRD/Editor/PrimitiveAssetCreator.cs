using System;
using System.Linq;
using _4XRD.Mesh;
using UnityEditor;

namespace _4XRD.Editor
{
    public class PrimitiveAssetCreator : EditorWindow
    {
        [MenuItem("Assets/Create/Mesh4D/Generate Primitive Meshes")]
        static void GeneratePrimitiveMeshes()
        {
            string directory = AssetDatabase.GUIDToAssetPath(Selection.assetGUIDs[0]);
            if (System.IO.Path.HasExtension(directory))
            {
                directory = System.IO.Path.GetDirectoryName(directory);
            }

            foreach (var type in Enum.GetValues(typeof(PrimitiveType4D)).Cast<PrimitiveType4D>())
            {
                string assetName = type + ".asset";
                if (directory != null)
                {
                    string assetPath = System.IO.Path.Combine(directory, assetName);
                    Mesh4D mesh4D = Mesh4D.CreatePrimitive(type);
                    AssetDatabase.CreateAsset(mesh4D, assetPath);
                }
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }
    }
}
