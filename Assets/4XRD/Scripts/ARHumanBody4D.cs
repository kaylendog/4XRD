using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARHumanBody4D : MonoBehaviour
{    
    public GameObject limbPrefab;
    
    private GameObject[] limbs;
    
    public void CreateBody(ARHumanBody body)
    {
        var joints = body.joints;
        if (!joints.IsCreated)
        {
            return;
        }

        limbs = new GameObject[joints.Length];
        for (int i = 0; i < joints.Length; i++)
        {
            XRHumanBodyJoint joint = joints[i];
            if(joint.parentIndex == -1)
            {
                limbs[i] = gameObject;
            }
            else
            {
                GameObject limb = Instantiate(limbPrefab, limbs[joint.parentIndex].transform);
                limbs[i] = limb;
            }
        }
    }

    public void ApplyBodyPose(ARHumanBody body)
    {
        transform.position = body.transform.position;
        transform.rotation = body.transform.rotation;
        
        var joints = body.joints;
        if (!joints.IsCreated)
        {
            return;
        }

        for (int i = 0; i < joints.Length; i++)
        {
            XRHumanBodyJoint joint = joints[i];
            if(joint.parentIndex == -1)
            {
                continue;
            }
            GameObject limb = limbs[i];
            GameObject limbParent = limbs[joint.parentIndex];
            
            limb.transform.localPosition = joint.localPose.position;
            limb.transform.LookAt(limbParent.transform.position);
            limb.transform.localScale = joint.localScale;

            float length = Vector3.Distance(limb.transform.position, limbParent.transform.position);
            float yScaling = length / limb.transform.lossyScale.y;
            Vector3 scale = limb.transform.localScale;
            scale.y *= yScaling;
            limb.transform.localScale = scale;
        }
    }
}
