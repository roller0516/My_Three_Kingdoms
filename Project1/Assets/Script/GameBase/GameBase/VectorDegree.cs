using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorDegree : MonoBehaviour
{
    public Transform transform1;
    public Transform transform2;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v1 = transform1.position - transform.position;
        Vector3 v2 = transform2.position - transform.position;
        float degree = (Mathf.Acos(Vector3.Dot(v1, v2) / Vector3.Magnitude(v1) / Vector3.Magnitude(v2)) * Mathf.Rad2Deg);

        float dot = Vector3.Dot(v1, v2);
        float mag = Vector3.Magnitude(v1) * Vector3.Magnitude(v2);
        print("" + degree  + "     "  +  mag);


    }
}
