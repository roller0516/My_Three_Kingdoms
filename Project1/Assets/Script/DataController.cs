using System.Collections.Generic;
using UnityEngine;
using System.Numerics;

public class DataController : MonoBehaviour
{
    #region Singleton
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
    #endregion
    BigInteger m_gold = 1000000000;
    BigInteger  m_goldperClick = 0;
    BigInteger  m_goldperClick1 = 0;
    BigInteger  m_goldperClick2 = 0;
    BigInteger  m_goldperClick3 = 0;
    BigInteger  m_Knowledge = 0;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        string gold;
        gold = m_gold.ToString();
        gold = PlayerPrefs.GetString("Gold", gold);
        m_gold = BigInteger.Parse(gold);

        string Knowledge;
        Knowledge = m_Knowledge.ToString();
        Knowledge = PlayerPrefs.GetString("Knowledge", Knowledge);
        m_Knowledge = BigInteger.Parse(Knowledge);

        List<string> key = new List<string>();

        key.Add(m_goldperClick.ToString());
        key.Add(m_goldperClick1.ToString());
        key.Add(m_goldperClick2.ToString());
        key.Add(m_goldperClick3.ToString());

        for (int i = 0; i < key.Count; i++)
        {
            key[i] = PlayerPrefs.GetString("GoldperClick" + i, key[i]);

            if(i == 0)
                m_goldperClick = BigInteger.Parse(key[0]);
            if (i == 1)
                m_goldperClick1 = BigInteger.Parse(key[1]);
            if (i == 2)
                m_goldperClick2 = BigInteger.Parse(key[2]);
            if (i == 3)
                m_goldperClick3 = BigInteger.Parse(key[3]);
        }
    }

        
    #region Gold
    public void SetGold(BigInteger newGold)
    {
        m_gold = newGold;
        PlayerPrefs.SetString("Gold", m_gold.ToString());
    }

    public void AddGold(BigInteger newGold)
    {
        m_gold = BigInteger.Add(m_gold, newGold);
        SetGold(m_gold);
    }
    public void SubGold(BigInteger newGold)
    {
        m_gold = BigInteger.Subtract(m_gold, newGold);
        print(m_gold);
        SetGold(m_gold);
    }
    public BigInteger GetGold()
    {
        return m_gold;
    }
    #endregion
    #region Knowledge
    public void SetKnowledge(BigInteger newKnowledge)
    {
        m_Knowledge = newKnowledge;
        PlayerPrefs.SetString("Knowledge", m_Knowledge.ToString());
    }

    public void AddKnowledge(BigInteger newKnowledge)
    {
        m_Knowledge = BigInteger.Add(m_Knowledge, newKnowledge);
        SetKnowledge(m_Knowledge);
    }
    public void SubKnowledge(BigInteger newKnowledge)
    {
        m_Knowledge = BigInteger.Subtract(m_Knowledge, newKnowledge);
        SetKnowledge(m_Knowledge);
    }
    public BigInteger GetKnowledge()
    {
        return m_Knowledge;
    }
    #endregion
    #region GoldPerClick
    public BigInteger GetGoldPerClick(string name)
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
    public void SetGoldPerClick(string name_, BigInteger newGoldPerClick)
    {
        string name = name_;
        print(name);
        if (name == "GoldPerClick0")
        {
            m_goldperClick = newGoldPerClick;
            PlayerPrefs.SetString("GoldPerClick0", m_goldperClick.ToString());
        }
        else if (name == "GoldPerClick1") 
        {
            m_goldperClick1 = newGoldPerClick;
            PlayerPrefs.SetString("GoldPerClick1", m_goldperClick1.ToString());
        }
        else if (name == "GoldPerClick2")
        {
            m_goldperClick2 = newGoldPerClick;
            PlayerPrefs.SetString("GoldPerClick2", m_goldperClick2.ToString());
        }
        else if (name == "GoldPerClick3")
        {
            m_goldperClick3 = newGoldPerClick;
            PlayerPrefs.SetString("GoldPerClick3", m_goldperClick3.ToString());
        }
    }
    public void AddGoldPerClick(string name_ , BigInteger newGoldPerClick)
    {
        string name = name_;

        if (name == "GoldPerClick0") 
        {
            m_goldperClick = BigInteger.Add(m_goldperClick, newGoldPerClick);
            SetGoldPerClick(name, m_goldperClick);
        }
        else if (name == "GoldPerClick1")
        {
            m_goldperClick1 = BigInteger.Add(m_goldperClick1, newGoldPerClick);
            SetGoldPerClick(name, m_goldperClick1);
        }
        else if (name == "GoldPerClick2")
        {
            m_goldperClick2 = BigInteger.Add(m_goldperClick2, newGoldPerClick);
            SetGoldPerClick(name, m_goldperClick2);
        }
        else if (name == "GoldPerClick3")
        {
            m_goldperClick3 = BigInteger.Add(m_goldperClick3, newGoldPerClick);
            SetGoldPerClick(name, m_goldperClick3);
        }
    }
    #endregion

    

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
        //upGradeButton.goldByUpgrade = PlayerPrefs.GetInt(key + "_goldByUpgrade", upGradeButton.StartGoldByUpgrade);
        //upGradeButton.CurrentCost = PlayerPrefs.GetInt(key + "+CurrentCost", upGradeButton.StartCurrentCost);
    }
    public void SaveUpgradeButton(UpgradeButton upGradeButton)
    {
        string key = upGradeButton.UpgradeName;

        PlayerPrefs.SetInt(key + "_Level", upGradeButton.Level);
        //PlayerPrefs.SetInt(key + "_goldByUpgrade", upGradeButton.goldByUpgrade);
        //PlayerPrefs.SetInt(key + "+CurrentCost", upGradeButton.CurrentCost);
    }
    public void Loaditem(ItemList itemlist) 
    {
        
        itemlist.item_Attack = PlayerPrefs.GetInt("itemAttack", itemlist.item_Attack);
        
        for (int i=0; i< itemlist.weaponData.dataArray.Length; i++)
        {
            string key = itemlist.weaponData.dataArray[i].UID;
            itemlist.weaponData.dataArray[i].Level = PlayerPrefs.GetInt(key, itemlist.weaponData.dataArray[i].Level);
        }
    }
    public void Saveitem(ItemList itemlist)
    {
        PlayerPrefs.SetInt("itemAttack", itemlist.item_Attack);
        for (int i = 0; i < itemlist.weaponData.dataArray.Length; i++)
        {
            string key = itemlist.weaponData.dataArray[i].UID;
            if(i == 0 )
                PlayerPrefs.SetInt(key, itemlist.weaponData.dataArray[0].Level+1);
            else
                PlayerPrefs.SetInt(key, itemlist.weaponData.dataArray[i].Level);
        }
    }
    public void LoadWeaponCost(Weaponcost weaponcost) 
    {
        string key = weaponcost.UpgradeName;
        weaponcost.CurrentCost = PlayerPrefs.GetInt(key + "WeaponCurrentCost", weaponcost.StartCost);
    }
    public void SaveWeaponCost(Weaponcost weaponcost)
    {
        string key = weaponcost.UpgradeName;
        PlayerPrefs.SetInt(key+"WeaponCurrentCost", weaponcost.CurrentCost);
    }
    public void LoadStage(MonsterSpawn mosterSpawn)
    {
        mosterSpawn.stg.curStage = PlayerPrefs.GetInt("Stage", 1);
        mosterSpawn.stg.MonsterCount = PlayerPrefs.GetInt("MonsterCount", 1);
    }
    public void SaveStage(MonsterSpawn mosterSpawn)
    {
        PlayerPrefs.SetInt("Stage", (int)mosterSpawn.stg.curStage);
        PlayerPrefs.SetInt("MonsterCount", (int)mosterSpawn.stg.MonsterCount);
    }
}




