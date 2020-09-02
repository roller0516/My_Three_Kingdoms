using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class GoldPerSec : MonoBehaviour
{
    public Text GoldText;
    private int Count = 2;
    private TimeScroll[] time_scroll;
    void Start()
    {
        time_scroll = new TimeScroll[Count];
        Count = 1;
        for (int i = 0; i < time_scroll.Length; ++i)
        {
            time_scroll[i] = GameObject.Find("TimeSlider" + Count).GetComponent<TimeScroll>();
            Count++;
        }
    }
   
    void Update()
    {
        for (int i = 0; i < time_scroll.Length; i++) 
        {
            if (time_scroll[i].isDone == true)
            {
                time_scroll[i].isDone = false;
                int goldPerClick = DataController.GetInstance().GetGoldPerClick("GoldperClick" + i);
                DataController.GetInstance().AddGold(goldPerClick);
            }
        }
        
    }
   
}
