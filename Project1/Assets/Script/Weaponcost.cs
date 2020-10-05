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

    private void Start()
    {
        
        DataController.GetInstance().LoadWeaponCost(this);
        UpdateUI();
    }

    public void PurChaseUpgrade(int num) //구매 함수
    {

        if (ItemList.Instance.weaponData.dataArray[num].Level < MaxLevel)
        {
            if (DataController.GetInstance().GetGold() >= CurrentCost)
            {
                DataController.GetInstance().SubGold(CurrentCost);
                ItemList.Instance.weaponData.dataArray[num].Level++;
                UpdateUpgrade(num);

                
                UpdateUI();
            }
        }
        DataController.GetInstance().SaveWeaponCost(this);
    }

    public void UpdateUpgrade(int num) // 업그레이드 공식
    {
       
            switch (ItemList.Instance.weaponData.dataArray[num].Level)
            {
                case 0:
                    CurrentCost = (int)Mathf.Pow(3 + 0.2f * (ItemList.Instance.weaponData.dataArray[num].Atk - 25), 2);
                    break;
                case 1:
                    CurrentCost = (int)Mathf.Pow(3 + 0.2f * (ItemList.Instance.weaponData.dataArray[num].Atk_2 - 25), 2);
                    break;
                case 2:
                    CurrentCost = (int)Mathf.Pow(3 + 0.2f * (ItemList.Instance.weaponData.dataArray[num].Atk_3 - 25), 2);
                    break;
                case 3:
                    CurrentCost = (int)Mathf.Pow(3 + 0.2f * (ItemList.Instance.weaponData.dataArray[num].Atk_4 - 25), 2);
                    break;
                case 4:
                    CurrentCost = (int)Mathf.Pow(3 + 0.2f * (ItemList.Instance.weaponData.dataArray[num].Atk_5 - 25), 2);
                    break;
                case 5:
                    CurrentCost = (int)Mathf.Pow(3 + 0.2f * (ItemList.Instance.weaponData.dataArray[num].Atk_6 - 25), 2);
                    break;
                case 6:
                    CurrentCost = (int)Mathf.Pow(3 + 0.2f * (ItemList.Instance.weaponData.dataArray[num].Atk_7 - 25), 2);
                    break;
                case 7:
                    CurrentCost = (int)Mathf.Pow(3 + 0.2f * (ItemList.Instance.weaponData.dataArray[num].Atk_8 - 25), 2);
                    break;
                case 8:
                    CurrentCost = (int)Mathf.Pow(3 + 0.2f * (ItemList.Instance.weaponData.dataArray[num].Atk_9 - 25), 2);
                    break;
                //case 9:
                //    CurrentCost = (int)Mathf.Pow(3 + 0.2f * (ItemList.Instance.weaponData.dataArray[num].Atk_10 - 25), 2);
                //    break;
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
        for (int i = 0; i < ItemList.Instance.weaponData.dataArray.Length; i ++)
        {
            if (ItemList.Instance.weaponData.dataArray[i].Level != MaxLevel)
            {
                if (DataController.GetInstance().GetGold() < CurrentCost)
                {
                    //Level_img.SetActive(false);
                    upGradeTex.color = Color.red;
                    //button_.image.color = new Color(180f / 255f, 180f / 255f, 180f / 255f, 255f / 255f);
                }
                else
                {
                    //Level_img.SetActive(true);
                    upGradeTex.color = Color.yellow;
                    //button_.image.color = Color.white;
                }
            }
            
        }
        
    }
}
