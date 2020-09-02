using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScroll : MonoBehaviour
{

    //public UpgradeButton UpGradeButton;
    public float MaxTime;
    public float CurTime = 0;
    public bool isDone = false;


   
    public void HandleTime(TimeScrollManager timescroll)
    {
        for (int i = 0; i < timescroll.Timebar.Length; i++)
        {
           timescroll.Timebar[i].value = (float)CurTime / (float)MaxTime;
        }
    }
    public void isDoneFuc()
    {
        CurTime += Time.deltaTime;
    }
    public void TimerValue(Slider slider)
    {
        slider.value = (float)CurTime / (float)MaxTime;
    }
}
