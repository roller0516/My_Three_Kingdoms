
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Numerics;


public class UIManager : MonoBehaviour
{
    public GameObject weaponTap;

    public TextMeshProUGUI Gold;
    public TextMeshProUGUI Knowledge;
    public TextMeshProUGUI[] GoldPerClickDisPlayer;
    public TextMeshProUGUI[] GoldCostClickDisPlayer;
    private UpgradeButton[] UpgradeButton = new UpgradeButton[4];
    private Weaponcost[] weaponcost = new Weaponcost[8];

    [HideInInspector]
    public int[] Level;
   
    private void Start()
    {
        for (int i = 1; i < UpgradeButton.Length+1; i++) 
        {
            UpgradeButton[i-1] = GameObject.Find("Button" + i).GetComponent<UpgradeButton>();
        }
    }
    private void Update()
    {
        for (int i=0; i < UpgradeButton.Length;i++) 
        {
            Level[i] = UpgradeButton[i].Level;
        }

        WeaponUpdate();
        Gold.text = GetGoldText();
        Knowledge.text = KnowledgeText();
        GoldPerClickText(GoldPerClickDisPlayer);
        GoldCostClickText(GoldCostClickDisPlayer);
        //GoldText((float)DataController.GetInstance().GetGold());
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
                if (ItemList.Instance.weaponData.dataArray[i].Level < ItemList.Instance.maxLevel && ItemList.Instance.weaponData.dataArray[i].Level > 0) 
                {
                    ItemList.Instance.bt[i].interactable = true;
                    weaponcost[i].upGradeTex.gameObject.SetActive(true);
                    ItemList.Instance.im[i].sprite = Resources.Load<Sprite>("UI/Training/nomalbutton");
                }
                else if (ItemList.Instance.weaponData.dataArray[i].Level == 10)
                {
                    ItemList.Instance.bt[i].interactable = false;
                    ItemList.Instance.im[i].sprite = Resources.Load<Sprite>("UI/Weapon/Complete");
                    weaponcost[i].upGradeTex.gameObject.SetActive(false);
                    if (i == ItemList.Instance.weaponData.dataArray.Length - 1)// i가 마지막일때는 return으로 빠져나간다.
                        return;
                    else if (ItemList.Instance.weaponData.dataArray[i + 1].Level == 0) 
                    {
                        weaponcost[i + 1].upGradeTex.gameObject.SetActive(true);
                        ItemList.Instance.im[i + 1].sprite = Resources.Load<Sprite>("UI/Training/nomalbutton");
                        ItemList.Instance.bt[i + 1].interactable = true;
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
    //a가 안나오는버그 수정

    public void GoldPerClickText(TextMeshProUGUI[] txt)
    {
        txt = GoldPerClickDisPlayer;

        for (int i = 0; i < GoldPerClickDisPlayer.Length; i++)
        {
            txt[i].text = GoldPerClickText("GoldperClick"+i);
        }
    }
    public void GoldCostClickText(TextMeshProUGUI[] txt)
    {
        txt = GoldCostClickDisPlayer;

        for (int i = 0; i < GoldPerClickDisPlayer.Length; i++)
        {
            txt[i].text = GoldCostClickText(UpgradeButton[i].CurrentCost);
        }
    }
}
