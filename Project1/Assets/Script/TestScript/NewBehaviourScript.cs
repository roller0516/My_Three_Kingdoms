using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class NewBehaviourScript : MonoBehaviour
{
    SkeletonRenderer SkeletonRenderer_;

    private void Start()
    {
        SkeletonRenderer_ = GetComponent<SkeletonRenderer>();
    }
    public void set_attechment()
    {
        SkeletonRenderer_.skeleton.SetAttachment("weapon 1", "weapon 2");
    }
}
