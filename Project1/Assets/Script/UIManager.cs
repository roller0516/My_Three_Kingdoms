
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Numerics;


public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    public static UIManager GetInstance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<UIManager>();
            if (instance == null)
            {
                GameObject container = new GameObject("Canvas");
                instance = container.AddComponent<UIManager>();
            }
        }
        return instance;
    }

    public GameObject weaponTap;
    public GameObject TeasureTap;

    //버튼 텍스트
    public TextMeshProUGUI Gold;
    
    public Text[] GoldPerClickDisPlayer;
    public Text[] GoldCostClickDisPlayer;
    public TextMeshProUGUI Knowledge;
    public TextMeshProUGUI[] Atktext;

    // 버튼 갯수
    public UpgradeButton[] upgradeButton = new UpgradeButton[20]; // 훈련버튼코스트
    public Weaponcost[] weaponcost = new Weaponcost[20]; // 무기 버튼 코스트
    public TeasureCostButton[] Teasurecost_Nomal = new TeasureCostButton[6];
    //public TeasureCostButton[] Teasurecost_Special = new TeasureCostButton[15];
    ItemList item_l;
    [HideInInspector]
    public int[] Level;
    private void Awake()
    {
        int count = 13;
        for (int i = 1; i < upgradeButton.Length + 1; i++)
        {
            upgradeButton[i - 1] = GameObject.Find("Button" + i).GetComponent<UpgradeButton>();
            upgradeButton[i - 1].UpgradeName = "Gold" + i;
            
            if (i == 1)
            {
                upgradeButton[i - 1].StartCurrentCost = "10";
            }
            else
            {
                upgradeButton[i - 1].StartCurrentCost = BigInteger.Multiply(BigInteger.Parse(upgradeButton[i - 2].StartCurrentCost.ToString()), count).ToString();
                count++;
            }
        }
    }
    private void Start()
    {
        Level = new int[upgradeButton.Length];
        item_l = FindObjectOfType<ItemList>().GetComponent<ItemList>();
    }
    private void Update()
    {
        for (int i=0; i < upgradeButton.Length;i++) 
        {
            Level[i] = upgradeButton[i].Level;
        }
        TeasureButton();
        WeaponUpdate();
        Gold.text = GetGoldText();
        Knowledge.text = KnowledgeText();
        GoldPerClickText(GoldPerClickDisPlayer);
        GoldCostClickText(GoldCostClickDisPlayer);
        AtkText(Atktext);
    }
    public void TeasureButton() 
    {
        if (TeasureTap.activeSelf == true) 
        {
            for (int i=0; i <Teasurecost_Nomal.Length;i++) 
            {
                Teasurecost_Nomal[i] = GameObject.Find("Treasure_b"+i).GetComponent<TeasureCostButton>();
            }
        }
    }
    public void WeaponUpdate() 
    {
        if (weaponTap.activeSelf == true)
        {
            for (int i = 1; i < weaponcost.Length + 1; i++)
            {
                weaponcost[i - 1] = GameObject.Find("Weapon" + i + "_b").GetComponent<Weaponcost>();
            }
            for (int i = 0; i < weaponcost.Length; i++)
            {
                if (item_l.weaponData.dataArray[i].Level < item_l.maxLevel && item_l.weaponData.dataArray[i].Level > 0) 
                {
                    item_l.bt[i].interactable = true;
                    weaponcost[i].upGradeTex.gameObject.SetActive(true);
                    item_l.im[i].sprite = Resources.Load<Sprite>("UI/Weapon/nomalbutton");
                }
                else if (item_l.weaponData.dataArray[i].Level == 10)
                {
                    item_l.bt[i].interactable = false;
                    item_l.im[i].sprite = Resources.Load<Sprite>("UI/Weapon/Complete");
                    weaponcost[i].upGradeTex.gameObject.SetActive(false);
                    if (i == item_l.weaponData.dataArray.Length - 1)// i가 마지막일때는 return으로 빠져나간다.
                        return;
                    else if (item_l.weaponData.dataArray[i + 1].Level == 0) 
                    {
                        weaponcost[i + 1].upGradeTex.gameObject.SetActive(true);
                        item_l.im[i + 1].sprite = Resources.Load<Sprite>("UI/Weapon/nomalbutton");
                        item_l.bt[i + 1].interactable = true;
                    }
                }
            }
        }
    }

    private string GetGoldText()
    {
        int placeN = 3;
        BigInteger value = DataController.GetInstance().GetGold();
        List<int> numlist = new List<int>();
        int p = (int)Mathf.Pow(10, placeN);

        do
        {
            numlist.Add((int)(value % p));
            value /= p;
        }
        while (value>=1);

        int num = numlist.Count < 2 ? numlist[0] : numlist[numlist.Count - 1] * p + numlist[numlist.Count - 2];

        if (num < 1000)
            return num.ToString();

        float f = (num / (float)p);

        return f.ToString("N2") + GetUnitText(numlist.Count - 1);
    }
    private string KnowledgeText()
    {
        int placeN = 3;
        BigInteger value = DataController.GetInstance().GetKnowledge();
        List<int> numlist = new List<int>();
        int p = (int)Mathf.Pow(10, placeN);

        do
        {
            numlist.Add((int)(value % p));
            value /= p;
        }
        while (value >= 1);

        int num = numlist.Count < 2 ? numlist[0] : numlist[numlist.Count - 1] * p + numlist[numlist.Count - 2];

        if (num < 1000)
            return num.ToString();

        float f = (num / (float)p);

        return f.ToString("N2") + GetUnitText(numlist.Count - 1);
    }


    private string GoldPerClickText(string name)
    {
        int placeN = 3;
        BigInteger value = DataController.GetInstance().GetGoldPerClick(name);
        List<int> numlist = new List<int>();
        int p = (int)Mathf.Pow(10, placeN);

        do
        {
            numlist.Add((int)(value % p));
            value /= p;
        }
        while (value >= 1);
        
        int num = numlist.Count < 2 ? numlist[0] : numlist[numlist.Count - 1] * p + numlist[numlist.Count - 2];



        if (num < 1000)
            return num.ToString();

        float f = (num / (float)p);

        return f.ToString("N2") + GetUnitText(numlist.Count - 1);
    }

    private string GoldCostClickText(BigInteger Cost)
    {
        int placeN = 3;
        BigInteger value = Cost;
        List<int> numlist = new List<int>();
        int p = (int)Mathf.Pow(10, placeN);

        do
        {
            numlist.Add((int)(value % p));
            value /= p;
        }
        while (value >= 1);

        int num = numlist.Count < 2 ? numlist[0] : numlist[numlist.Count - 1] * p + numlist[numlist.Count - 2];

        if (num < 1000)
            return num.ToString();

        float f = (num / (float)p);

        return f.ToString("N2") + GetUnitText(numlist.Count - 1);
    }
    private string AtkText(BigInteger Atk)
    {
        int placeN = 3;
        BigInteger value = Atk;
        List<int> numlist = new List<int>();
        int p = (int)Mathf.Pow(10, placeN);

        do
        {
            numlist.Add((int)(value % p));
            value /= p;
        }
        while (value >= 1);

        int num = numlist.Count < 2 ? numlist[0] : numlist[numlist.Count - 1] * p + numlist[numlist.Count - 2];

        if (num < 1000)
            return num.ToString();

        float f = (num / (float)p);

        return f.ToString("N2") + GetUnitText(numlist.Count - 1);
    }

    private string GetUnitText(int index)
    {
        int idx = index - 1;

        if (idx < 0)
            return "";

        int repeatCount = (index / 26) + 1;

        string retstr = "";

        for (int i = 0; i < repeatCount; i++)
        {
            retstr += (char)(64 + index % 26);
        }
        return retstr;
    }

    public void GoldPerClickText(Text[] txt)
    {
        //txt = GoldPerClickDisPlayer;

        for (int i = 0; i < GoldPerClickDisPlayer.Length; i++)
        {
            txt[i].text = GoldPerClickText("GoldPerClick"+i);
        }
    }
    public void GoldCostClickText(Text[] txt)
    {
        //txt = GoldCostClickDisPlayer;

        for (int i = 0; i < GoldPerClickDisPlayer.Length; i++)
        {
            txt[i].text = GoldCostClickText(upgradeButton[i].CurrentCost);
        }
    }
    public void AtkText(TextMeshProUGUI[] txt)
    {
        //txt = Atktext;

        for (int i = 0; i < item_l.weaponData.dataArray.Length; i++)
        {
            switch (item_l.weaponData.dataArray[i].Level)
            {
                case 0:
                    txt[i].text = AtkText(BigInteger.Parse(item_l.weaponData.dataArray[i].Atk));
                    break;
                case 1:
                    txt[i].text = AtkText(BigInteger.Parse(item_l.weaponData.dataArray[i].Atk_2));
                    break;
                case 2:
                    txt[i].text = AtkText(BigInteger.Parse(item_l.weaponData.dataArray[i].Atk_3));
                    break;
                case 3:
                    txt[i].text = AtkText(BigInteger.Parse(item_l.weaponData.dataArray[i].Atk_4));
                    break;
                case 4:
                    txt[i].text = AtkText(BigInteger.Parse(item_l.weaponData.dataArray[i].Atk_5));
                    break;
                case 5:
                    txt[i].text = AtkText(BigInteger.Parse(item_l.weaponData.dataArray[i].Atk_6));
                    break;
                case 6:
                    txt[i].text = AtkText(BigInteger.Parse(item_l.weaponData.dataArray[i].Atk_7));
                    break;
                case 7:
                    txt[i].text = AtkText(BigInteger.Parse(item_l.weaponData.dataArray[i].Atk_8));
                    break;
                case 8:
                    txt[i].text = AtkText(BigInteger.Parse(item_l.weaponData.dataArray[i].Atk_9));
                    break;
                case 9:
                    txt[i].text = AtkText(BigInteger.Parse(item_l.weaponData.dataArray[i].Atk_10));
                    break;
                case 10:
                    txt[i].text = AtkText(BigInteger.Parse(item_l.weaponData.dataArray[i].Atk_10));
                    break;
            }
        } 
    }
}
