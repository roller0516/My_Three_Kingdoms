using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KShake : MonoBehaviour
{
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform orgTransform;
        
    public float shakeTime      = 0f;      // How long the object should shake for.
    public float shakeAmount    = 0.6f;    // Amplitude of the shake.
    public float shakeDecay     = 0.0f;    // decrease shakeAmount per sec

    Vector3 originalPos;

    void Awake()
    {
        orgTransform = transform;
    }
    void OnEnable()
    {
        originalPos = orgTransform.localPosition;
    }
    void Update()
    {
        if (shakeTime > 0)
        {
            shakeTime -= Time.deltaTime;

            var pos = originalPos + Random.insideUnitSphere * shakeAmount; pos.z = originalPos.z; //2d
            transform.localPosition = pos;
                        
            shakeAmount -= Time.deltaTime * shakeDecay;            
        }
        else
        {
            shakeTime = 0f;
            transform.localPosition = originalPos;
        }
    }

    public void StartShake(float _sec, float _amount, float _decrease = 0f )
    {
        shakeTime = _sec;
        shakeAmount = _amount;
        shakeDecay = _decrease;
    }
    public void StopShake()
    {
        shakeTime = 0;
        transform.localPosition = originalPos;        
    }

}
