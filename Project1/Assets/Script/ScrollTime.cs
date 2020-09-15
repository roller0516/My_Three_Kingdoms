using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollTime : MonoBehaviour
{
    public float MaxTime;
    public float CurTime = 0;
    [HideInInspector]
    public bool isDone = false;

    public void CurTimeFuc() // 시간을 계산
    {
        CurTime += Time.deltaTime;
        if (CurTime >= MaxTime)
        {
            CurTime = 0;
            isDone = true;
        }
        else
            isDone = false;
    }
}
