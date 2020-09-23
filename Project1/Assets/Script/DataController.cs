using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class DataController : MonoBehaviour
{
    private static DataController instance;

    public static DataController GetInstance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<DataController>();
            if (instance == null)
            {
                GameObject container = new GameObject("DataController");
                instance = container.AddComponent<DataController>();
            }
        }
        return instance;
    }

    private int m_gold = 0;
    private int m_goldperClick = 0;
    private int m_goldperClick1 = 0;
    private int m_goldperClick2 = 0;
    private int m_goldperClick3 = 0;

   

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        m_gold = PlayerPrefs.GetInt("Gold", 100000);
        m_goldperClick = PlayerPrefs.GetInt("GoldPerClick0", 9);
        m_goldperClick1 = PlayerPrefs.GetInt("GoldPerClick1", 41);
        m_goldperClick2 = PlayerPrefs.GetInt("GoldPerClick2", 632);
        m_goldperClick3 = PlayerPrefs.GetInt("GoldPerClick3", 9600);
    }
    
    public void SetGold(int newGold)
    {
        m_gold = newGold;
        PlayerPrefs.SetInt("Gold", m_gold);
    }

    public void AddGold(int newGold)
    {
        m_gold += newGold;
        SetGold(m_gold);
    }
    public void SubGold(int newGold)
    {
        m_gold -= newGold;
        SetGold(m_gold);
    }
    public int GetGold()
    {
        return m_gold;
    }
    public int GetGoldPerClick(string name)
    {
        string num = name;

        if (num == "GoldperClick0")
            return m_goldperClick;
        else if (num == "GoldperClick1")
            return m_goldperClick1;
        else if (num == "GoldperClick2")
            return m_goldperClick2;
        else if (num == "GoldperClick3")
            return m_goldperClick3;
        return 0;
    }
    public void SetGoldPerClick(string name_,int newGoldPerClick)
    {
        string name = name_;
        if (name == "GoldPerClick0")
        {
            m_goldperClick = newGoldPerClick;
            PlayerPrefs.SetInt("GoldPerClick0", m_goldperClick);
        }
        else if (name == "GoldPerClick1") 
        {
            m_goldperClick1 = newGoldPerClick;
            PlayerPrefs.SetInt("GoldPerClick1", m_goldperClick1);
        }
        else if (name == "GoldPerClick2")
        {
            m_goldperClick2 = newGoldPerClick;
            PlayerPrefs.SetInt("GoldPerClick2", m_goldperClick2);
        }
        else if (name == "GoldPerClick3")
        {
            m_goldperClick3 = newGoldPerClick;
            PlayerPrefs.SetInt("GoldPerClick3", m_goldperClick3);
        }
    }
    public void AddGoldPerClick(string name_ ,int newGoldPerClick)
    {
        string name = name_;

        if (name == "GoldPerClick0") 
        {
            m_goldperClick += newGoldPerClick;
            SetGoldPerClick(name, m_goldperClick);
        }
        else if (name == "GoldPerClick1")
        {
            m_goldperClick1 += newGoldPerClick;
            SetGoldPerClick(name, m_goldperClick1);
        }
        else if (name == "GoldPerClick2")
        {
            m_goldperClick2 += newGoldPerClick;
            SetGoldPerClick(name, m_goldperClick2);
        }
        else if (name == "GoldPerClick3")
        {
            m_goldperClick3 += newGoldPerClick;
            SetGoldPerClick(name, m_goldperClick3);
        }
    }

    public void LoadUpgradeButton(UpgradeButton upGradeButton)
    {
        string key = upGradeButton.UpgradeName;
        
        if (key == "Gold")
        {
            upGradeButton.Level = PlayerPrefs.GetInt(key+"_Level", 0);
        }
        else if (key == "Gold1") 
        {
            upGradeButton.Level = PlayerPrefs.GetInt(key + "_Level", 0);
        }
        else if (key == "Gold2")
        {
            upGradeButton.Level = PlayerPrefs.GetInt(key + "_Level", 0);
        }
        else if (key == "Gold3")
        {
            upGradeButton.Level = PlayerPrefs.GetInt(key + "_Level", 0);
        }
        upGradeButton.goldByUpgrade = PlayerPrefs.GetInt(key + "_goldByUpgrade", upGradeButton.StartGoldByUpgrade);
        upGradeButton.CurrentCost = PlayerPrefs.GetInt(key + "+CurrentCost", upGradeButton.StartCurrentCost);
    }
    public void SaveUpgradeButton(UpgradeButton upGradeButton)
    {
        string key = upGradeButton.UpgradeName;

        PlayerPrefs.SetInt(key + "_Level", upGradeButton.Level);
        PlayerPrefs.SetInt(key + "_goldByUpgrade", upGradeButton.goldByUpgrade);
        PlayerPrefs.SetInt(key + "+CurrentCost", upGradeButton.CurrentCost);

    }
    //public void LoadUpgradeButton(ItemList itemlist)
    //{
    //    string[] Value = new string[itemlist.AllitemList.Count];



    //    for (int i =0;i< itemlist.AllitemList.Count;i++)
    //    {
    //        string key = itemlist.AllitemList[i].name;
    //        itemlist.AllitemList[i].name = PlayerPrefs.GetString(key + "item", itemlist.AllitemList[i].name);
    //        itemlist.AllitemList[i].level = PlayerPrefs.GetInt(key + "_Level", itemlist.AllitemList[i].level);
    //        itemlist.WeaponGradeSlider[i].value = PlayerPrefs.GetFloat(key + "UpgradeValue", itemlist.WeaponGradeSlider[i].value);
    //        Value[i]= PlayerPrefs.GetString(key + "itemPuton", itemlist.AllitemList[i].isUsing.ToString());
    //        itemlist.AllitemList[i].isUsing = System.Convert.ToBoolean(Value[i]);
    //    }
    //    itemlist.item_Attack = PlayerPrefs.GetInt("WeaponUpgradeValue", itemlist.item_Attack);
    //    itemlist.AttackUpgrade =  PlayerPrefs.GetInt("Upgrade", 0);
    //}
    
    //public void SaveUpgradeButton(ItemList itemlist)
    //{
    //    for (int i = 0; i< itemlist.AllitemList.Count;i++)
    //    {
    //        string key = itemlist.AllitemList[i].name;
    //        PlayerPrefs.SetString(key + "item", itemlist.AllitemList[i].name);
    //        PlayerPrefs.SetInt(key + "_Level", itemlist.AllitemList[i].level);
    //        PlayerPrefs.SetFloat(key + "UpgradeValue",itemlist.WeaponGradeSlider[i].value);
    //        PlayerPrefs.SetString(key + "itemPuton", itemlist.AllitemList[i].isUsing.ToString());
    //    }
    //    PlayerPrefs.SetInt("WeaponUpgradeValue", itemlist.item_Attack);
    //    PlayerPrefs.SetInt("Upgrade", itemlist.AttackUpgrade);
    //}
}




