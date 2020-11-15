// kdw 2011.10.31.
// 특정대상을 타겟을 향하여 일정각도와 거리에 위치시키는 카메라 콘트롤. 

using UnityEngine;
using System.Collections;

public class KCameraController : MonoBehaviour {
	
	//---------------------------------------------
	public Transform	_cameraTransform;
	public Transform	_targetTransform;

    public Vector3      targetOffset = new Vector3(0, 1, 0);    //위치

    public float		fDistance = 15.0f;          //Editor 우선 참조
	public float		fPitch = 30.0f;
	public float		fYaw = 0.0f;

    float minimumX = -360F;
    float maximumX = 360F;
    float minimumY = -0F;   // fPitch max
    float maximumY = 90F;
    float minimumZ = 1F;    // fDistance max
    float maximumZ = 30F;
    

    private float 		fVelocity = 0.0f;
    float               fDistance_cur;
    float               fSmoothTime = 0.1F;	// smooth, smaller is faster.
    
	private float 		angleVelocity = 0.0f;
	private float 		angularSmoothTime = 0.2f;
	private float 		angularMaxSpeed = 15.0f;	
		
	//---------------------------------------------
	//enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	//RotationAxes axes = RotationAxes.MouseXAndY;
	//float sensitivityX = 15F;
	//float sensitivityY = 15F;
	//float sensitivityZ = 1.5F;
		
	//float rotationY = 0F;
	
	//---------------------------------------------
	void Awake () 
	{
		if(!_cameraTransform && Camera.main)
			_cameraTransform = Camera.main.transform;
		if(!_cameraTransform) {
			Debug.Log("Please assign a camera to the CameraController script.");
			enabled = false;	
		}

        //_targetTransform = transform;		

        if (!_targetTransform)
        {
            Debug.Log("Please assign a target Transform.");
        }

    }
	
	void Start () {
	
	}

	void Update () {
/*		
		if( Input.GetMouseButton(1) ) //우클릭으로 카메라 조정
		{		
			fYaw	+= Input.GetAxis("Mouse X");
			//fYaw = Mathf.Clamp (fYaw, minimumX, maximumX);			
			fPitch	-= Input.GetAxis("Mouse Y");
			fPitch = Mathf.Clamp (fPitch, minimumY, maximumY);
		}	
*/		
		fDistance -= Input.GetAxis("Mouse ScrollWheel");
		fDistance = Mathf.Clamp (fDistance, minimumZ, maximumZ); 
		
	}	
	
	void LateUpdate () {
		
		if(!_targetTransform)	return;
		
		Vector3 targetPos = ( _targetTransform.position + targetOffset );
		
		// target y angle
		//float currentAngle	= _cameraTransform.eulerAngles.y;
		//float targetAngle 	= _targetTransform.eulerAngles.y;
		//currentAngle = Mathf.SmoothDampAngle(currentAngle, targetAngle, ref angleVelocity, angularSmoothTime, angularMaxSpeed);				
		//Quaternion currentRotation = Quaternion.Euler (fPitch, currentAngle + fYaw, 0);

		Quaternion currentRotation = Quaternion.Euler (fPitch, fYaw, 0);
		
		//_cameraTransform.position = targetPos + currentRotation * Vector3.back * fDistance;
		fDistance_cur = Mathf.SmoothDamp(fDistance_cur, fDistance, ref fVelocity, fSmoothTime);
		_cameraTransform.position = targetPos + currentRotation * Vector3.back * fDistance_cur;
		
				
		Vector3 relativePos = targetPos - _cameraTransform.position;		
		_cameraTransform.rotation = Quaternion.LookRotation(relativePos); // * Quaternion.Euler (fPitch, fYaw, 0 );
	
	}	
	
	float AngleDistance ( float a, float b )
	{
		a = Mathf.Repeat(a, 360);
		b = Mathf.Repeat(b, 360);		
		return Mathf.Abs(b - a);
	}	
}

// look target
//Vector3 relativePos = target.position - transform.position;
//Quaternion rotation = Quaternion.LookRotation(relativePos);
//transform.rotation = rotation;
