using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GNBbutton : MonoBehaviour
{
    float Speed = 0;
    int num = 1;

    Vector3 StartPos;
    Vector3 CurrentPos;
    Vector3 TargetPos;

    RectTransform rt;
   
    bool AlphaColor = false;

    public Image[] ImgList;
    
    private void Start()
    {
        rt = GetComponent<RectTransform>();
        print(rt.anchoredPosition);
        StartPos = rt.anchoredPosition;
        TargetPos = rt.anchoredPosition;
        CurrentPos= rt.anchoredPosition;
        ImgList = this.transform.GetComponentsInChildren<Image>();
    }
    private void Update()
    {
        CurrentPos = Vector3.Lerp(CurrentPos, TargetPos, Time.deltaTime*3);
        rt.anchoredPosition = CurrentPos;
    }
    public void GNBUpDown()
    {
        if (num == 1)
        {
            GNBup();
            num++;
        }
        else if (num == 2)
        {
            GNBDown();
            num = 1;
            rt.anchoredPosition = StartPos;
        }
    }
    public void GNBup()  //내가 원하는 좌표까지
    {
        TargetPos = new Vector3(StartPos.x, -52, 0);
        //for (int i = 0; i < ImgList.Length; i++)
        //{
        //    if (i == 0)
        //        continue;
        //    ImgList[i].color=new Color(ImgList[i].color.r / 255, ImgList[i].color.g / 255, ImgList[i].color.b / 255, Color_.a);
        //}
    }
    public void GNBDown() //다시 시작점으로
    {
        TargetPos = new Vector3(StartPos.x, StartPos.y, 0);
    }
}
