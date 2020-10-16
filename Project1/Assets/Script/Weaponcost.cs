using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Numerics;

public class Weaponcost : MonoBehaviour
{
    public string UpgradeName;
    [HideInInspector]
    public BigInteger CurrentCost;
    [HideInInspector]
    public int goldByUpgrade;
    public int StartCost;
    [HideInInspector]
    public int MaxLevel = 10;
    public TextMeshProUGUI upGradeTex;
    ItemList item_l;
    
    private void Start()
    {
        CurrentCost = StartCost;
        item_l = FindObjectOfType<ItemList>().GetComponent<ItemList>();
        DataController.GetInstance().LoadWeaponCost(this);
        UpdateUI();
    }

    public void PurChaseUpgrade(int num) //구매 함수
    {
        SoundManager.instance.ButtonSound();
        if (item_l.weaponData.dataArray[num].Level < MaxLevel)
        {
            if (DataController.GetInstance().GetGold() >= CurrentCost)
            {
                DataController.GetInstance().SubGold(CurrentCost);
                
                UpdateUpgrade(num);

                
                UpdateUI();
                item_l.weaponData.dataArray[num].Level++;
            }
        }
        DataController.GetInstance().SaveWeaponCost(this);
    }

    public void UpdateUpgrade(int num) // 업그레이드 공식
    {
       switch (num)
       {
           case 0:
               CurrentCost += 50;
               break;
           case 1:
                CurrentCost += 110;
               break;
           case 2:
                CurrentCost += 330;
               break;
           case 3:
                CurrentCost += 990;
               break;
           case 4:
                CurrentCost += 2970;
               break;
           case 5:
                CurrentCost += 8910;
               break;
           case 6:
                CurrentCost += 26730;
               break;
           case 7:
                CurrentCost += 80190;
               break;
           case 8:
               CurrentCost += 240570;
               break;
           case 9:
               CurrentCost += 721710;
               break;
           case 10:
               CurrentCost += 2165130;
               break;
           case 11:
               CurrentCost += 6495390;
               break;
           case 12:
               CurrentCost += 19486170;
               break;
           case 13:
               CurrentCost += 58458510;
               break;
           case 14:
               CurrentCost += 175375530;
               break;
           case 15:
               CurrentCost += 526126590;
               break;
           case 16:
               CurrentCost += 1578379770;
               break;
           case 17:
                CurrentCost += 4735139310;
               break;
           case 18:
               CurrentCost += 14205417930;
               break;
           case 19:
                CurrentCost += 42616253790;
                break;
        }
    }
    public void UpdateUI()//ui의 변화를 받아온다
    {
        upGradeTex.text = "" + CurrentCost;
    }
    private void Update()
    {
        ScarceCost_textColor();
    }
    public void ScarceCost_textColor()//재화 부족시 컬러변경
    {
        for (int i = 0; i < item_l.weaponData.dataArray.Length; i ++)
        {
            if (item_l.weaponData.dataArray[i].Level != MaxLevel)
            {
                if (DataController.GetInstance().GetGold() < CurrentCost)
                {
                    upGradeTex.color = Color.red;
                }
                else
                {
                    upGradeTex.color = Color.yellow;
                }
            }
            
        }
    }
}
