using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Numerics;
using Vector3 = UnityEngine.Vector3;

public class TeasureCostButton : MonoBehaviour
{

    public string UpgradeNameText = "";
    public string UpgradeName;
    [HideInInspector]
    public int CurrentCost;
    [HideInInspector]
    public int goldByUpgrade;

    [HideInInspector]
    public int Level = 0;
    public int MaxLevel;


    public Text LevelTex;
    public TextMeshProUGUI upGradeTex;
    public TextMeshProUGUI EffectTex;
    public Button button_;
    public GameObject img;
    //public GameObject Level_img;

    public int StartKnowledgeByUpgrade;//처음 지식 업그레이드양
    public int KnowledgeByUpgrade;// 지식 증가량
    public int StartCurrentCost;

    private void Start()
    {
        CurrentCost = StartCurrentCost;
        DataController.GetInstance().LoadTeasure(this);
        UpdateUI();
    }

    public void PurChaseUpgrade() //구매 함수
    {
        SoundManager.instance.Purchase();
        if (Level < MaxLevel)
        {
            if (DataController.GetInstance().GetKnowledge() >= CurrentCost)
            {
            
                DataController.GetInstance().SubKnowledge(CurrentCost);
                Level++;
                UpdateUpgrade();

                TeasureAbility(UpgradeName);
                UpdateUI();
                DataController.GetInstance().SaveTeasure(this);
            }
        }
    }

    
    public void UpdateUI()//ui의 변화를 받아온다
    {
        LevelTex.text =  Level.ToString();

        upGradeTex.text = "" + CurrentCost;
        if(UpgradeName == "treasure_8")
            EffectTex.text = UpgradeNameText+ (goldByUpgrade+100) + "%";
        else
            EffectTex.text = UpgradeNameText + goldByUpgrade + "%";

        if (Level == MaxLevel)
        {
            upGradeTex.rectTransform.anchoredPosition = new Vector3(0, 0, 0);
            button_.image.sprite = Resources.Load<Sprite>("UI/Treasure/maxButton");
            LevelTex.text = "Lv" + "." + MaxLevel.ToString();
            button_.interactable = false;
        }
    }
    private void Update()
    {
        ScarceCost_textColor();
    }
    public void ScarceCost_textColor()//재화 부족시 컬러변경
    {
        
        if (Level < MaxLevel)
        {
            if (DataController.GetInstance().GetKnowledge() < CurrentCost)
            {
                upGradeTex.color = Color.red;
                button_.image.color = new Color(180f / 255f, 180f / 255f, 180f / 255f, 255f / 255f);
            }
            else
            {
                upGradeTex.color = Color.yellow;
                button_.image.color = Color.white;
            }
        }
        else if (Level == MaxLevel)
        {
            img.SetActive(false);
            upGradeTex.gameObject.SetActive(false);
        }
        if (Level>0)
        {
            EffectTex.color = new Color(70f / 255f, 255f / 255f, 0f / 255f, 255f / 255f);
        }
    }
    public void UpdateUpgrade() // 업그레이드 공식
    {
        CurrentCost += ((StartCurrentCost * 125) / 100);// 지불하는 값을 업그레이드
        goldByUpgrade += KnowledgeByUpgrade;
    }
    public void TeasureAbility(string name)
    {
        switch (name)
        {
            case "treasure_1":
                for (int i = 0; i < DataController.GetInstance().key.Count;i++)
                {
                    BigInteger num;
                    BigInteger num2;
                    num = DataController.GetInstance().GetGoldPerClick("GoldPerClick" + i);
                    num2 = BigInteger.Parse(UIManager.GetInstance().upgradeButton[i].GoldByUpgrade);
                    if (UIManager.GetInstance().upgradeButton[i].Level > 0) 
                    {
                        DataController.GetInstance().SetGoldPerClick("GoldPerClick" + i, num + (num2 * goldByUpgrade * UIManager.GetInstance().upgradeButton[i].Level * 100) / 10000);
                        UIManager.GetInstance().upgradeButton[i].goldByUpgrade = DataController.GetInstance().GetGoldPerClick("GoldPerClick" + i);
                        DataController.GetInstance().SaveUpgradeButton(UIManager.GetInstance().upgradeButton[i]);
                    }
                        print(DataController.GetInstance().GetGoldPerClick("GoldPerClick" + i));
                }
                DataController.GetInstance().Teasure1Ability += 1;
                PlayerPrefs.SetInt("Teasure1Ability", DataController.GetInstance().Teasure1Ability);
                break;
            case "treasure_2":
                break;
            case "treasure_3":
                Player.Instance.Critical += 1;
                PlayerPrefs.SetInt("Critical", Player.Instance.Critical);
                break;
            case "treasure_4":
                for (int i = 0; i < UIManager.GetInstance().upgradeButton.Length; i++) 
                {
                    BigInteger num;
                    BigInteger num2;
                    num = UIManager.GetInstance().upgradeButton[i].CurrentCost;
                    num2 = num * goldByUpgrade;
                    UIManager.GetInstance().upgradeButton[i].CurrentCost1 = ((num * 100) - num2) / 100;
                    DataController.GetInstance().SaveUpgradeButton(UIManager.GetInstance().upgradeButton[i]);
                }
                DataController.GetInstance().Teasure2Ability += 1;
                PlayerPrefs.SetInt("Teasure2Ability", DataController.GetInstance().Teasure2Ability);
                break;
            case "treasure_5":
                float time1 = GameObject.Find("SkillButton").GetComponent<SKillCooltime>().CrurrentTime;
                float time = GameObject.Find("SkillButton").GetComponent<SKillCooltime>().MaxSkillcooltime;
                time1 = time * (1-((float)goldByUpgrade / 100));
                GameObject.Find("SkillButton").GetComponent<SKillCooltime>().CrurrentTime = time1;
                PlayerPrefs.SetFloat("skill", GameObject.Find("SkillButton").GetComponent<SKillCooltime>().CrurrentTime);

                break;
            case "treasure_6":
                GameObject.Find("CreatureSkillButton").GetComponent<CreatureSummon>().CrurrentTime -= 0.5f;
                PlayerPrefs.SetFloat("CreatureSkillButton", GameObject.Find("CreatureSkillButton").GetComponent<CreatureSummon>().CrurrentTime);
                break;
            case "treasure_8":
                Player.Instance.CriticalPer += 2;
                
                PlayerPrefs.SetInt("CriticalPer", Player.Instance.CriticalPer);
                break;
        }
    }
}