using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroll : MonoBehaviour
{
  
    private MeshRenderer render;
    public float Speed;
    private float offset;

    void Start()
    {
    

        render = GetComponent<MeshRenderer>();
    }

    void Update()
    {
       
        offset += Time.deltaTime * Speed;
        render.material.mainTextureOffset = new Vector2(offset, 0);
    }
}
