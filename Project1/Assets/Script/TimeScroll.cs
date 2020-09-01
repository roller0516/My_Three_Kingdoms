using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScroll : MonoBehaviour
{
    private Slider Timebar;
    public UpgradeButton UpGradeButton;
    public float MaxTime;
    public float CurTime = 0;
    public bool isDone = false;
   

    void Start()
    {
        Timebar = GetComponent<Slider>();
        Timebar.value = (float)CurTime / (float)MaxTime;
        UpGradeButton = GameObject.Find("Button1").GetComponent<UpgradeButton>();
    }
    void Update()
    {
        if (UpGradeButton.Level > 0)
        {
            CurTime += Time.deltaTime;
            if (CurTime >= MaxTime)
            {
                CurTime = 0;
                this.isDone = true;
            }
            HandleTime();
        }
    }
    void HandleTime() 
    {
        this.Timebar.value = (float)CurTime / (float)MaxTime;
    }
}
