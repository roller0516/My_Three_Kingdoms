using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TeasureCostButton : MonoBehaviour
{
    public string UpgradeName = "";

    [HideInInspector]
    public int CurrentCost;
    [HideInInspector]
    public int goldByUpgrade;

    [HideInInspector]
    public int Level = 0;

    public int MaxLevel;


    public TextMeshProUGUI LevelTex;
    public TextMeshProUGUI upGradeTex;
    public TextMeshProUGUI EffectTex;
    public Button button_;
    //public GameObject Level_img;


    public int StartKnowledgeByUpgrade;//처음 지식 업그레이드양
    public int KnowledgeByUpgrade;// 지식 증가량
    public int StartCurrentCost;

    private void Start()
    {
        CurrentCost = StartCurrentCost;
        //DataController.GetInstance().LoadWeaponCost(this);
        UpdateUI();
    }

    public void PurChaseUpgrade() //구매 함수
    {
        if (Level < MaxLevel)
        {
            if (DataController.GetInstance().GetKnowledge() >= CurrentCost)
            {
            
                DataController.GetInstance().SubKnowledge(CurrentCost);
                print(CurrentCost);
                Level++;
                UpdateUpgrade();
                

                UpdateUI();
                //DataController.GetInstance().SaveWeaponCost(this);
            }
        }
    }

    
    public void UpdateUI()//ui의 변화를 받아온다
    {
        LevelTex.text = "Lv" + "." + Level.ToString();

        upGradeTex.text = "" + CurrentCost;

        EffectTex.text = UpgradeName+goldByUpgrade+"%";

        if (Level == MaxLevel)
        {
            upGradeTex.rectTransform.anchoredPosition = new Vector3(0, 0, 0);
            upGradeTex.text = "Max";
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
            upGradeTex.color = Color.yellow;
        }
        if (Level>0)
        {
            EffectTex.color = new Color(70f / 255f, 255f / 255f, 0f / 255f, 255f / 255f);
        }
    }
    public void UpdateUpgrade() // 업그레이드 공식
    {
        CurrentCost += ((StartCurrentCost * 106) / 100); // 지불하는 값을 업그레이드
        goldByUpgrade += KnowledgeByUpgrade;
    }
}