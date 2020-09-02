using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    public string UpgradeName;

   
    public Button button_;
    public Text LevelTex;
    public Text upGradeTex;

    [HideInInspector]
    public int goldByUpgrade;
    [HideInInspector]
    public int goldByUpgrade1;

    public int StartGoldByUpgrade =1;

    [HideInInspector]
    public int CurrentCost = 1;

    public int StartCurrentCost;
    [HideInInspector]
    public int Level = 0;
    [HideInInspector]
    public int MaxLevel = 100;

    public float UpgradePow = 1.07f; //골드 획득량을 증가시켜주는 변수

    public float costPow = 3.14f;

    private void Start()
    {
        DataController.GetInstance().LoadUpgradeButton(this);
        UpdateUI();
    }
    private void Update()
    {
        ScarceCost_textColor();
        DataController.GetInstance().GoldPerClickDisPlayer[0].text = "" + DataController.GetInstance().GetGoldPerClick("GoldperClick0");
        DataController.GetInstance().GoldPerClickDisPlayer[1].text = "" + DataController.GetInstance().GetGoldPerClick("GoldperClick1");
        DataController.GetInstance().GoldPerClickDisPlayer[2].text = "" + DataController.GetInstance().GetGoldPerClick("GoldperClick2");
        DataController.GetInstance().GoldPerClickDisPlayer[3].text = "" + DataController.GetInstance().GetGoldPerClick("GoldperClick3");
    }
    public void PurChaseUpgrade() //구매 함수
    {
        
        if (Level < MaxLevel)
        {
            if (DataController.GetInstance().GetGold() >= CurrentCost)
            {
                DataController.GetInstance().SubGold(CurrentCost);
                Level++;
                DataController.GetInstance().AddGoldPerClick("GoldPerClick0", goldByUpgrade);

                UpdateUpgrade();
                UpdateUI();
                DataController.GetInstance().SaveUpgradeButton(this);

            }
        }
    }
    public void PurChaseUpgrade1()//구매 함수 1
    {
        if (Level < MaxLevel)
        {
            if (DataController.GetInstance().GetGold() >= CurrentCost)
            {
                DataController.GetInstance().SubGold(CurrentCost);
                Level++;
                DataController.GetInstance().AddGoldPerClick("GoldPerClick1", goldByUpgrade);

                UpdateUpgrade();
                UpdateUI();
                DataController.GetInstance().SaveUpgradeButton(this);

            }
        }
    }
    public void PurChaseUpgrade2()//구매 함수 2
    {
        if (Level < MaxLevel)
        {
            print(Level);
            if (DataController.GetInstance().GetGold() >= CurrentCost)
            {
                DataController.GetInstance().SubGold(CurrentCost);
                Level++;
                DataController.GetInstance().AddGoldPerClick("GoldPerClick2", goldByUpgrade);

                UpdateUpgrade();
                UpdateUI();
                DataController.GetInstance().SaveUpgradeButton(this);

            }
        }
    }
    public void PurChaseUpgrade3()//구매 함수 3
    {
        if (Level < MaxLevel)
        {
            print(Level);
            if (DataController.GetInstance().GetGold() >= CurrentCost)
            {
                DataController.GetInstance().SubGold(CurrentCost);
                Level++;
                DataController.GetInstance().AddGoldPerClick("GoldPerClick3", goldByUpgrade);

                UpdateUpgrade();
                UpdateUI();
                DataController.GetInstance().SaveUpgradeButton(this);

            }
        }
    }
    public void UpdateUpgrade() // 업그레이드 공식
    {
        goldByUpgrade = StartGoldByUpgrade *(int) Mathf.Pow(UpgradePow, Level);//Mathf.Pow는 제곱이다.
        goldByUpgrade1 = StartGoldByUpgrade * (int)Mathf.Pow(UpgradePow+1, Level);
        CurrentCost = StartCurrentCost * (int)Mathf.Pow(costPow, Level); // 식량 공식
    }
    
    public void ScarceCost_textColor()//재화 부족시 컬러변경
    {
        if (Level != MaxLevel)
        {
            if (DataController.GetInstance().GetGold() < CurrentCost)
            {
                upGradeTex.color = Color.red;
                button_.image.color = Color.gray;
            }
            else
            {
                upGradeTex.color = Color.yellow;
                button_.image.color = Color.red;
            }
        }
    }
    public void UpdateUI()//ui의 변화를 받아온다
    {
        LevelTex.text = "Lv" + Level.ToString();

        upGradeTex.text = "" + CurrentCost;

        if (Level == MaxLevel)
        {
            upGradeTex.rectTransform.anchoredPosition = new Vector3(0, 0, 0);
            upGradeTex.text = "최대레벨";
            LevelTex.text = "최대레벨";
            button_.image.color = Color.gray;
            button_.interactable = false;
            GameObject go = GameObject.Find("FoodIm").gameObject;
            Destroy(go);
        }
    }
}
