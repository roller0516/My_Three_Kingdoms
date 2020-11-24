using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollTimebarManager : MonoBehaviour
{
    public Slider[] Timebar;
    public ScrollTime[] time;
    private void Update()
    {
        HandleTime();
        //각 스크립트마다 레벨을 받아서 실행시킨다 
        ButtonLevel();
    }
    public void HandleTime()
    {
        for (int i = 0; i < Timebar.Length; i++) 
        {
            Timebar[i].value = (float)time[i].CurTime / (float)time[i].MaxTime;
        }
    }
    void ButtonLevel() 
    {
        for (int i = 0; i < Timebar.Length; i++)
        {
            if (UIManager.GetInstance().Level[i] > 0)
                time[i].CurTimeFuc();
        }
    }
}
