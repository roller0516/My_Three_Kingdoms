using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select_Ani : MonoBehaviour
{
    public Animation anim;
    // Update is called once per frame

    private void Start()
    {
        anim = gameObject.GetComponent<Animation>();
    }
}
