using NUnit.Framework;
using UnityEngine;

using _4XRD.Transform;

public class Transform4DTest
{
    Vector4 v = new(1, 2, 3, 4);
    Transform4D a = new(
        new Vector4(1, 2, 3, 4),
        new Vector4(4, 3, 2, 1),
        Rotation4x4.FromAngles(new Euler6(Mathf.PI/4, -Mathf.PI/4, Mathf.PI/4, -Mathf.PI/4, Mathf.PI/4, -Mathf.PI/4))
    );
    Transform4D b = new(
        new Vector4(2, 2, 5, 1),
        new Vector4(2, 3, 2, 6),
        Rotation4x4.FromAngles(new Euler6(-Mathf.PI/8, -Mathf.PI/8, Mathf.PI/8, -Mathf.PI/8, Mathf.PI/8, -Mathf.PI/8))
    );
    
    [Test]
    public void Transform4DComposition()
    {
        Vector4 c = a * (b * v);
        Vector4 d = (a * b) * v;
        Assert.AreEqual(c, d);
    }
}
