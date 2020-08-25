using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldPerSec : MonoBehaviour
{
    public Text GoldText;
    private int Count = 4;
    [SerializeField]
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
        if (time_scroll[0].isDone == true)
        {
            time_scroll[0].isDone = false;
            int goldPerClick = DataController.GetInstance().GetGoldPerClick();
            DataController.GetInstance().AddGold(goldPerClick);
        }
        if (time_scroll[1].isDone == true)
        {
            time_scroll[1].isDone = false;
            int goldPerClick = DataController.GetInstance().GetGoldPerClick();
            DataController.GetInstance().AddGold(goldPerClick);
        }
        if (time_scroll[2].isDone == true)
        {
            time_scroll[2].isDone = false;
            int goldPerClick = DataController.GetInstance().GetGoldPerClick();
            DataController.GetInstance().AddGold(goldPerClick);
        }
        if (time_scroll[3] != null && time_scroll[3].isDone == true)
        {
            time_scroll[3].isDone = false;
            int goldPerClick = DataController.GetInstance().GetGoldPerClick();
            DataController.GetInstance().AddGold(goldPerClick);
        }

    }
   
}
