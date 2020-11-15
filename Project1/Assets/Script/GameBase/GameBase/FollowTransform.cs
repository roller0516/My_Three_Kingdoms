using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour {

    public Transform _targetTransform;
    
    float fDistance_cur;
    float fSmoothTime = 0.0F;    // smooth, smaller is faster. //보정
    private float fVelocity = 0.0f;

    private Vector3 Velocity = Vector3.zero;

    public Vector3 targetOffset = new Vector3(0, 0, 0);

    private float angleVelocity = 0.0f;
    private float angularSmoothTime = 0.2f;
    private float angularMaxSpeed = 10.0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void LateUpdate()
    {
        if (!_targetTransform) return;

        // AR 카메라 추적
        Vector3 targetPos = (_targetTransform.position + targetOffset); 
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref Velocity, fSmoothTime);                
        transform.rotation = Quaternion.Slerp(transform.rotation, _targetTransform.rotation, Time.deltaTime * angularSmoothTime);
        
    }
}
