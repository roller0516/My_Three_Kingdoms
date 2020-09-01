using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScrollManager : MonoBehaviour
{
    public Slider[] Timebar;
    TimeScroll timeScroll;
    private UpgradeButton upgradebutton;
    private void Start()
    {
        timeScroll = GameObject.Find("TimeSlider1").GetComponent<TimeScroll>();
        upgradebutton = GameObject.Find("Button1").GetComponent<UpgradeButton>();
        timeScroll.HandleTime(this);
    }
    private void Update()
    {
        timeScroll.isDoneFuc();
        
        if (upgradebutton.Level > 0)
        {
            timeScroll.TimerValue(Timebar[0]);

            if (timeScroll.CurTime >= timeScroll.MaxTime)
            {
                timeScroll.CurTime = 0;
                timeScroll.isDone = true;
                print(upgradebutton.Level);
            }
            else
                timeScroll.isDone = false;
            
        }
        if (upgradebutton.Level2 > 1)
        {
            timeScroll.TimerValue(Timebar[1]);

            if (timeScroll.CurTime >= timeScroll.MaxTime)
            {
                timeScroll.CurTime = 0;
                timeScroll.isDone = true;
                print(upgradebutton.Level2);
            }
            else
                timeScroll.isDone = false;
        }
        
       
    }
}
