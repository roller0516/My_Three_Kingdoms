using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Weaponcost : MonoBehaviour
{
    public string UpgradeName;
    [HideInInspector]
    public int CurrentCost = 1;
    [HideInInspector]
    public int goldByUpgrade;
    public int StartCost;
    [HideInInspector]
    public int MaxLevel = 10;
    public TextMeshProUGUI upGradeTex;
    ItemList item_l;
    private void Start()
    {
        item_l = FindObjectOfType<ItemList>().GetComponent<ItemList>();
        DataController.GetInstance().LoadWeaponCost(this);
        UpdateUI();
    }

    public void PurChaseUpgrade(int num) //구매 함수
    {

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
               CurrentCost +=50;
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
