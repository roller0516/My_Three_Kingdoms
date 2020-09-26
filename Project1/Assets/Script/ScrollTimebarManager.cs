﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollTimebarManager : MonoBehaviour
{
    public Slider[] Timebar;
    public ScrollTime[] time;
    UIManager uimanager;
    private void Start()
    {
        uimanager = GameObject.FindWithTag("Canvas").GetComponent<UIManager>();
    }
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
        if (uimanager.Level[0] > 0)
            time[0].CurTimeFuc();
        if (uimanager.Level[1] > 0)
            time[1].CurTimeFuc();
        if (uimanager.Level[2] > 0)
            time[2].CurTimeFuc();
        if (uimanager.Level[3] > 0)
            time[3].CurTimeFuc();
    }
}