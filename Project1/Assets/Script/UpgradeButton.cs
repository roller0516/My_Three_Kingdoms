
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Numerics;
using Common;
using Vector3 = UnityEngine.Vector3;


public class UpgradeButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler

{
    
    public string UpgradeName;

    [HideInInspector]
    public BigInteger CurrentCost = 1;
    [HideInInspector]
    public BigInteger goldByUpgrade;

    [HideInInspector]
    public int Level = 0;

    public int MaxLevel = 1000;

    
    public TextMeshProUGUI LevelTex;
    public TextMeshProUGUI upGradeTex;
    public Button button_;
    public GameObject Level_img;
    public int StartGoldByUpgrade =1;
    public int StartCurrentCost;

    float CurTime;

    public float UpgradePow = 1.07f; //골드 획득량을 증가시켜주는 변수

    public float costPow = 3.14f;

    bool PressDown = false;

    private void Start()
    {
        DataController.GetInstance().LoadUpgradeButton(this);
        Level_img = transform.Find("LevelUp_img").gameObject;
        UpdateUI();
    }

    public void PurChaseUpgrade() //구매 함수
    {
        
        if (Level < MaxLevel)
        {
            if (DataController.GetInstance().GetGold() >= CurrentCost)
            {
                DataController.GetInstance().SubGold(CurrentCost);
                Level++;
                UpdateUpgrade();
                DataController.GetInstance().SetGoldPerClick("GoldPerClick0", goldByUpgrade);

                
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
                UpdateUpgrade();
                DataController.GetInstance().SetGoldPerClick("GoldPerClick1", goldByUpgrade);

                
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
                UpdateUpgrade();
                DataController.GetInstance().SetGoldPerClick("GoldPerClick2", goldByUpgrade);

               
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
                UpdateUpgrade();
                DataController.GetInstance().SetGoldPerClick("GoldPerClick3", goldByUpgrade);

               
                UpdateUI();
                DataController.GetInstance().SaveUpgradeButton(this);
            }
        }
    }
    public void UpdateUpgrade() // 업그레이드 공식
    {
        CurrentCost = (BigInteger)Mathf.Round(Mathf.Pow(UpgradePow + (costPow * Level), 2));///Mathf.Pow는 제곱이다.
        goldByUpgrade = BigInteger.Divide(CurrentCost, 6);
    }

    public void ScarceCost_textColor()//재화 부족시 컬러변경
    {
        if (Level != MaxLevel)
        {
            if (DataController.GetInstance().GetGold() < CurrentCost)
            {
                Level_img.SetActive(false);
                upGradeTex.color = Color.red;
                button_.image.color = new Color(180f / 255f, 180f / 255f, 180f / 255f, 255f / 255f);
            }
            else
            {
                Level_img.SetActive(true);
                upGradeTex.color = Color.yellow;
                button_.image.color = Color.white;
            }
        }
        if (Level == MaxLevel)
        {
            Level_img.SetActive(false);
            upGradeTex.color = Color.yellow;
        }
    }
    public void Update()
    {
        ScarceCost_textColor();
        if (PressDown == true)
        {
            CurTime += Time.deltaTime ;
            if (CurTime >= 0.5f)
            {
                ButtonOn();
            }
        }
        
            
    }
    public void UpdateUI()//ui의 변화를 받아온다
    {
        

        LevelTex.text = "Lv" +"."+ Level.ToString();

        upGradeTex.text = "" + CurrentCost;

        if (Level == MaxLevel)
        {
            upGradeTex.rectTransform.anchoredPosition = new Vector3(0, 0, 0);
            upGradeTex.text = "최대레벨";
            LevelTex.text = "Lv"+"."+MaxLevel.ToString();
            //button_.image.color = Color.gray;
            button_.interactable = false;
            
        }
    }
    public void ButtonOn()
    {
        switch (UpgradeName)
        {
            case "Gold":
                PurChaseUpgrade();
                break;
            case "Gold1":
                PurChaseUpgrade1();
                break;
            case "Gold2":
                PurChaseUpgrade2();
                break;
            case "Gold3":
                PurChaseUpgrade3();
                break;

        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        PressDown = false;
        CurTime = 0;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PressDown = true;
        
        
    }
    
}
