using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    public Text upGradeTex;
    public Text LevelTex;
    public Button[] button_;

    public string UpgradeName;

    [HideInInspector]

    public int goldByUpgrade;

    public int StartGoldByUpgrade =1;

    [HideInInspector]

    public int CurrentCost = 1;

    public int StartCurrentCost;
    [HideInInspector]
    public int Level;
    public int MaxLevel = 100;

    public float UpgradePow = 1.07f; //골드 획득량을 증가시켜주는 변수

    public float costPow = 3.14f;


    private void Start()
    {
        //CurrentCost = StartCurrentCost;
        //Level = 1;
        //goldByUpgrade = StartGoldByUpgrade;
        DataController.GetInstance().LoadUpgradeButton(this);
        UpdateUI();
    }
    private void Update()
    {
        ScarceCost_textColor();
    }
    public void PurChaseUpgrade()
    {
        if (Level < MaxLevel)
        {
            if (DataController.GetInstance().GetGold() >= CurrentCost)
            {
                DataController.GetInstance().SubGold(CurrentCost);
                Level++;
                DataController.GetInstance().AddGoldPerClick(goldByUpgrade);

                UpdateUpgrade();
                UpdateUI();
                DataController.GetInstance().SaveUpgradeButton(this);
                print("구매했습니다");
            }
            else
            {
                print("돈이부족합니다");
            }
        }
    }

    public void UpdateUpgrade() // 업그레이드 공식
    {
        goldByUpgrade = StartGoldByUpgrade *(int) Mathf.Pow(UpgradePow, Level); //Mathf.Pow는 제곱이다.
        CurrentCost = StartCurrentCost * (int)Mathf.Pow(UpgradePow, Level); // 식량 공식
    }
    public void UpdateUI()
    {
        upGradeTex.text = "" + CurrentCost;
        LevelTex.text ="Lv"+ Level.ToString();
        if (Level == MaxLevel)
        {
            upGradeTex.rectTransform.anchoredPosition = new Vector3(0, 0, 0);
            upGradeTex.text = "최대레벨";
            LevelTex.text = "최대레벨";
            button_[0].image.color = Color.gray;
            button_[0].interactable = false;
            GameObject go = GameObject.Find("FoodIm").gameObject;
            Destroy(go);
        }
            
    }
    public void ScarceCost_textColor()//재화 부족시 컬러변경
    {
        if (Level != MaxLevel)
        {
            if (DataController.GetInstance().GetGold() < CurrentCost)
            {
                upGradeTex.color = Color.red;
                button_[0].image.color = Color.gray;
            }
            else
            {
                upGradeTex.color = Color.yellow;
                button_[0].image.color = Color.red;
            }
        }
    }
}
