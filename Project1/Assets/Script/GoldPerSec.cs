using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using System.Numerics;

public class GoldPerSec : MonoBehaviour
{
    private int Count = 20;
    private ScrollTime[] time_scroll;

    void Start()
    {
        time_scroll = new ScrollTime[Count];

        Count = 1;

        for (int i = 0; i < time_scroll.Length; ++i)
        {
            time_scroll[i] = GameObject.Find("TimeSlider" + Count).GetComponent<ScrollTime>();
            Count++;
        }
    }
    void Update() // 눌렀을때 올라가는 골드량을 받아와서 현재 골드량과 상승 골드량을 더해주어 보내줌
    {
        for (int i = 0; i < time_scroll.Length; i++) 
        {
            if (time_scroll[i].isDone == true)
            {
                time_scroll[i].isDone = false;

                BigInteger goldPerClick = DataController.GetInstance().GetGoldPerClick("GoldPerClick" + i);
                
                DataController.GetInstance().AddGold(goldPerClick);
            }
        }
    }
}
