
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Numerics;
using Common;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;


public class UpgradeButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler

{
    public string UpgradeName = "";

    [HideInInspector]
    public BigInteger CurrentCost;
    [HideInInspector]
    public BigInteger CurrentCost1;
    [HideInInspector]
    public BigInteger goldByUpgrade;
    public GameObject ParentsTr;
    public GameObject FX;
    public RectTransform ParentsRr;
    [HideInInspector]
    public int Level = 0;

    public int MaxLevel = 1000;
    BigInteger Teasure1;
    BigInteger Teasure2;
    BigInteger num;

    public TextMeshProUGUI LevelTex;
    public TextMeshProUGUI upGradeTex;
    public Button button_;
    public GameObject Level_img;

    public string StartGoldByUpgrade;
    public string GoldByUpgrade;
    public string StartCurrentCost;
    
    float CurTime;
    bool PressDown = false;
    private void Start()
    {
        DataController.GetInstance().LoadUpgradeButton(this);
        Level_img = transform.Find("LevelUp_img").gameObject;
        UpdateUI();
    }
    public void UpgradeTik(int num)
    {
        Teasure1 = BigInteger.Multiply(Level, BigInteger.Multiply(BigInteger.Multiply(DataController.GetInstance().Teasure1Ability, 100), BigInteger.Parse(GoldByUpgrade)));
        DataController.GetInstance().SetGoldPerClick("GoldPerClick" + num, (BigInteger.Divide(Teasure1, 10000)) + goldByUpgrade);
        Teasure2 = BigInteger.Multiply(DataController.GetInstance().Teasure2Ability, CurrentCost);
        CurrentCost1 = ((CurrentCost * 100) - Teasure2) / 100;
    }
    public void PurChaseUpgrade(int num) //구매 함수
    {
        SoundManager.instance.Purchase();
        if (Level < MaxLevel)
        {
            if (DataController.GetInstance().GetGold() >= CurrentCost1)
            {
                ParentsRr = ParentsTr.GetComponent<RectTransform>();
                GameObject Clone = Instantiate(FX, ParentsRr.position, Quaternion.identity);
                Clone.transform.SetParent(ParentsTr.transform);
                DataController.GetInstance().SubGold(CurrentCost1);
                Level++;
                Destroy(Clone, 1f);
                UpdateUpgrade();
                
                DataController.GetInstance().SetGoldPerClick("GoldPerClick"+num, (BigInteger.Divide(Teasure1,10000))+goldByUpgrade);
                goldByUpgrade = (BigInteger.Divide(Teasure1, 10000)) + goldByUpgrade;
                UpdateUI();
                DataController.GetInstance().SaveUpgradeButton(this);
            }
        }
    }
    public void UpdateUpgrade() // 업그레이드 공식
    {
        CurrentCost = BigInteger.Divide((BigInteger.Multiply(CurrentCost, 112)), 100);
        Teasure2 = BigInteger.Multiply(DataController.GetInstance().Teasure2Ability, CurrentCost);
        CurrentCost1 = ((CurrentCost*100)- Teasure2)/100;
        goldByUpgrade += BigInteger.Parse(GoldByUpgrade);
        Teasure1 = BigInteger.Multiply(Level,BigInteger.Multiply(BigInteger.Multiply(DataController.GetInstance().Teasure1Ability, 100), BigInteger.Parse(GoldByUpgrade)));
        
    }

    public void ScarceCost_textColor()//재화 부족시 컬러변경
    {
        if (Level != MaxLevel)
        {
            if (DataController.GetInstance().GetGold() < CurrentCost1)
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
            upGradeTex.gameObject.SetActive(false);
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
        LevelTex.text = "Lv."+Level.ToString();

        upGradeTex.text = "" + CurrentCost1;

        if (Level == MaxLevel)
        {
            upGradeTex.rectTransform.anchoredPosition = new Vector3(0, 0, 0);
            button_.image.sprite = Resources.Load<Sprite>("UI/Treasure/maxButton");
            LevelTex.text = "Lv"+"."+MaxLevel.ToString();
            button_.interactable = false;
        }
    }
    public void ButtonOn()
    {
        for (int i = 1; i < 21; i++)
        {
            if (UpgradeName == "Gold" + i)
                    PurChaseUpgrade(i-1);
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
