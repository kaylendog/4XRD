using NUnit.Framework;
using UnityEngine;

using _4XRD.Transform;

public class Transform4DTest
{
    Vector4 v = new(1, 2, 3, 4);
    Transform4D a = new(
        new Vector4(1, 2, 3, 4),
        new Vector4(4, 3, 2, 1),
        new Euler6(Mathf.PI/4, -Mathf.PI/4, Mathf.PI/4, -Mathf.PI/4, Mathf.PI/4, -Mathf.PI/4)
    );
    Transform4D b = new(
        new Vector4(2, 2, 5, 1),
        new Vector4(2, 3, 2, 6),
        new Euler6(-Mathf.PI/8, -Mathf.PI/8, Mathf.PI/8, -Mathf.PI/8, Mathf.PI/8, -Mathf.PI/8)
    );
    Transform4D translateOnly = new(
        new Vector4(1, 2, 3, 4),
        Vector4.one,
        Euler6.zero
    );
    Transform4D scaleOnly = new(
        Vector4.zero,
        new Vector4(4, 3, 2, 1),
        Euler6.zero
    );
    Transform4D rotateOnly = new(
        Vector4.zero,
        Vector4.one,
        new Euler6(Mathf.PI/4, -Mathf.PI/4, Mathf.PI/4, -Mathf.PI/4, Mathf.PI/4, -Mathf.PI/4)
    );

    Rotation4x4 r = Rotation4x4.FromAngles(new Euler6(Mathf.PI/4, -Mathf.PI/8, Mathf.PI/4, -Mathf.PI/4, Mathf.PI/4, -Mathf.PI/8));

    [Test]
    public void Transform4DApply()
    {
        Vector4 x, y;
        
        x = translateOnly.Apply(v);
        Assert.True(x == new Vector4(2, 4, 6, 8));

        x = scaleOnly.Apply(v);
        Assert.True(x == new Vector4(4, 6, 6, 4));

        x = translateOnly.Apply(rotateOnly.Apply(scaleOnly.Apply(v)));
        y = a.Apply(v);
        Assert.True(x == y);
    }
    
    [Test]
    public void Transform4DApplyInverse()
    {
        Vector4 x, vPrime;
        
        x = a.Apply(v);
        vPrime = a.ApplyInverse(x);
        Assert.True(v == vPrime);

        x = b.Apply(v);
        vPrime = b.ApplyInverse(x);
        Assert.True(v == vPrime);
    }
}
