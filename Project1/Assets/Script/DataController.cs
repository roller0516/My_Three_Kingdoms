using System.Collections.Generic;
using UnityEngine;
using System.Numerics;
using Common;

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

    BigInteger m_gold = 0;
    BigInteger m_goldperClick = 6;
    BigInteger m_goldperClick1 = 144;
    BigInteger m_goldperClick2 = 2520 ;
    BigInteger m_goldperClick3 = 40800 ;
    BigInteger m_goldperClick4 = 1200000 ;
    BigInteger m_goldperClick5 = 24000000 ;
    BigInteger m_goldperClick6 = 480000000 ;
    BigInteger m_goldperClick7 = 1440000000 ;
    BigInteger m_goldperClick8 = 37800000000 ;
    BigInteger m_goldperClick9 = 156000000000 ;
    BigInteger m_goldperClick10 = 4800000000000 ;
    BigInteger m_goldperClick11 = 22000000000000 ;
    BigInteger m_goldperClick12 = 660000000000000 ;
    BigInteger m_goldperClick13 = 46000000000000000;
    BigInteger m_goldperClick14 = 27000000000000000;
    BigInteger m_goldperClick15;
    BigInteger m_goldperClick16;
    BigInteger m_goldperClick17;
    BigInteger m_goldperClick18;
    BigInteger m_goldperClick19;
    BigInteger m_Knowledge;
 



    private void Awake()
    {

        DontDestroyOnLoad(this.gameObject);
        
        m_goldperClick15 = BigInteger.Multiply(m_goldperClick14, 27);
        m_goldperClick16 = BigInteger.Multiply(m_goldperClick15, 28);
        m_goldperClick17 = BigInteger.Multiply(m_goldperClick16, 29);
        m_goldperClick18 = BigInteger.Multiply(m_goldperClick17, 30);
        m_goldperClick19 = BigInteger.Multiply(m_goldperClick18, 31);

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
        key.Add(m_goldperClick4.ToString());
        key.Add(m_goldperClick5.ToString());
        key.Add(m_goldperClick6.ToString());
        key.Add(m_goldperClick7.ToString());
        key.Add(m_goldperClick8 .ToString());
        key.Add(m_goldperClick9 .ToString());
        key.Add(m_goldperClick10.ToString());
        key.Add(m_goldperClick11.ToString());
        key.Add(m_goldperClick12.ToString());
        key.Add(m_goldperClick13.ToString());
        key.Add(m_goldperClick14.ToString());
        key.Add(m_goldperClick15.ToString());
        key.Add(m_goldperClick16.ToString());
        key.Add(m_goldperClick17.ToString());
        key.Add(m_goldperClick18.ToString());
        key.Add(m_goldperClick19.ToString());


        for (int i = 0; i < key.Count; i++)
        {
            key[i] = PlayerPrefs.GetString("GoldPerClick" + i, key[i]);
            switch (i)
            {
                case 0:
                    m_goldperClick = BigInteger.Parse(key[i]);
                    break;
                case 1:
                    m_goldperClick1 = BigInteger.Parse(key[i]);
                    break;
                case 2:
                    m_goldperClick2 = BigInteger.Parse(key[i]);
                    break;
                case 3:
                    m_goldperClick3 = BigInteger.Parse(key[i]);
                    break;
                case 4:
                    m_goldperClick4 = BigInteger.Parse(key[i]);
                    break;
                case 5:
                    m_goldperClick5 = BigInteger.Parse(key[i]);
                    break;
                case 6:
                    m_goldperClick6 = BigInteger.Parse(key[i]);
                    break;
                case 7:
                    m_goldperClick7 = BigInteger.Parse(key[i]);
                    break;
                case 8:
                    m_goldperClick8 = BigInteger.Parse(key[i]);
                    break;
                case 9:
                    m_goldperClick9 = BigInteger.Parse(key[i]);
                    break;
                case 10:
                    m_goldperClick10 = BigInteger.Parse(key[i]);
                    break;
                case 11:
                    m_goldperClick11 = BigInteger.Parse(key[i]);
                    break;
                case 12:
                    m_goldperClick12 = BigInteger.Parse(key[i]);
                    break;
                case 13:
                    m_goldperClick13 = BigInteger.Parse(key[i]);
                    break;
                case 14:
                    m_goldperClick14 = BigInteger.Parse(key[i]);
                    break;
                case 15:
                    m_goldperClick15 = BigInteger.Parse(key[i]);
                    break;
                case 16:
                    m_goldperClick16 = BigInteger.Parse(key[i]);
                    break;
                case 17:
                    m_goldperClick17 = BigInteger.Parse(key[i]);
                    break;
                case 18:
                    m_goldperClick18 = BigInteger.Parse(key[i]);
                    break;
                case 19:
                    m_goldperClick19 = BigInteger.Parse(key[i]);
                    break;
            }
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
        print(newKnowledge);
        m_Knowledge = BigInteger.Subtract(m_Knowledge, newKnowledge);
        print(m_Knowledge);
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

        if (num == "GoldPerClick0")
            return m_goldperClick;
        else if (num == "GoldPerClick1")
            return m_goldperClick1;
        else if (num == "GoldPerClick2")
            return m_goldperClick2;
        else if (num == "GoldPerClick3")
            return m_goldperClick3;
        else if (num == "GoldPerClick4")
            return m_goldperClick4;
        else if (num == "GoldPerClick5")
            return m_goldperClick5;
        else if (num == "GoldPerClick6")
            return m_goldperClick6;
        else if (num == "GoldPerClick7")
            return m_goldperClick7;
        else if (num == "GoldPerClick8")
            return m_goldperClick8;
        else if (num == "GoldPerClick9")
            return m_goldperClick9;
        else if (num == "GoldPerClick10")
            return m_goldperClick10;
        else if (num == "GoldPerClick11")
            return m_goldperClick11;
        else if (num == "GoldPerClick12")
            return m_goldperClick12;
        else if (num == "GoldPerClick13")
            return m_goldperClick13;
        else if (num == "GoldPerClick14")
            return m_goldperClick14;
        else if (num == "GoldPerClick15")
            return m_goldperClick15;
        else if (num == "GoldPerClick16")
            return m_goldperClick16;
        else if (num == "GoldPerClick17")
            return m_goldperClick17;
        else if (num == "GoldPerClick18")
            return m_goldperClick18;
        else if (num == "GoldPerClick19")
            return m_goldperClick19;
        return 0;
    }
    public void SetGoldPerClick(string name_, BigInteger newGoldPerClick)
    {
        string name = name_;
        
        if (name == "GoldPerClick0")
        {
            m_goldperClick = newGoldPerClick;
            PlayerPrefs.SetString("GoldPerClick0", m_goldperClick.ToString());
        }
        else if (name == "GoldPerClick1") 
        {
            print(name);
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
        else if (name == "GoldPerClick4")
        {
            m_goldperClick4 = newGoldPerClick;
            PlayerPrefs.SetString("GoldPerClick4", m_goldperClick4.ToString());
        }
        else if (name == "GoldPerClick5")
        {
            m_goldperClick5 = newGoldPerClick;
            PlayerPrefs.SetString("GoldPerClick5", m_goldperClick5.ToString());
        }
        else if (name == "GoldPerClick6")
        {
            m_goldperClick6 = newGoldPerClick;
            PlayerPrefs.SetString("GoldPerClick6", m_goldperClick6.ToString());
        }
        else if (name == "GoldPerClick7")
        {
            m_goldperClick7 = newGoldPerClick;
            PlayerPrefs.SetString("GoldPerClick7", m_goldperClick7.ToString());
        }
        else if (name == "GoldPerClick8")
        {
            m_goldperClick8 = newGoldPerClick;
            PlayerPrefs.SetString("GoldPerClick8", m_goldperClick8.ToString());
        }
        else if (name == "GoldPerClick9")
        {
            m_goldperClick9 = newGoldPerClick;
            PlayerPrefs.SetString("GoldPerClick9", m_goldperClick9.ToString());
        }
        else if (name == "GoldPerClick10")
        {
            m_goldperClick10 = newGoldPerClick;
            PlayerPrefs.SetString("GoldPerClick10", m_goldperClick10.ToString());
        }
        else if (name == "GoldPerClick11")
        {
            m_goldperClick11 = newGoldPerClick;
            PlayerPrefs.SetString("GoldPerClick11", m_goldperClick11.ToString());
        }
        else if (name == "GoldPerClick12")
        {
            m_goldperClick12 = newGoldPerClick;
            PlayerPrefs.SetString("GoldPerClick12", m_goldperClick12.ToString());
        }
        else if (name == "GoldPerClick13")
        {
            m_goldperClick13 = newGoldPerClick;
            PlayerPrefs.SetString("GoldPerClick13", m_goldperClick13.ToString());
        }
        else if (name == "GoldPerClick14")
        {
            m_goldperClick14 = newGoldPerClick;
            PlayerPrefs.SetString("GoldPerClick14", m_goldperClick14.ToString());
        }
        else if (name == "GoldPerClick15")
        {
            m_goldperClick15 = newGoldPerClick;
            PlayerPrefs.SetString("GoldPerClick15", m_goldperClick15.ToString());
        }
        else if (name == "GoldPerClick16")
        {
            m_goldperClick16 = newGoldPerClick;
            PlayerPrefs.SetString("GoldPerClick16", m_goldperClick16.ToString());
        }
        else if (name == "GoldPerClick17")
        {
            m_goldperClick17 = newGoldPerClick;
            PlayerPrefs.SetString("GoldPerClick17", m_goldperClick17.ToString());
        }
        else if (name == "GoldPerClick18")
        {
            m_goldperClick18 = newGoldPerClick;
            PlayerPrefs.SetString("GoldPerClick18", m_goldperClick18.ToString());
        }
        else if (name == "GoldPerClick19")
        {
            m_goldperClick19 = newGoldPerClick;
            PlayerPrefs.SetString("GoldPerClick19", m_goldperClick19.ToString());
        }

    }
    //public void AddGoldPerClick(string name_ , BigInteger newGoldPerClick)
    //{
    //    string name = name_;

    //    if (name == "GoldPerClick0") 
    //    {
    //        m_goldperClick = BigInteger.Add(m_goldperClick, newGoldPerClick);
    //        SetGoldPerClick(name, m_goldperClick);
    //    }
    //    else if (name == "GoldPerClick1")
    //    {
    //        m_goldperClick1 = BigInteger.Add(m_goldperClick1, newGoldPerClick);
    //        SetGoldPerClick(name, m_goldperClick1);
    //    }
    //    else if (name == "GoldPerClick2")
    //    {
    //        m_goldperClick2 = BigInteger.Add(m_goldperClick2, newGoldPerClick);
    //        SetGoldPerClick(name, m_goldperClick2);
    //    }
        
    #endregion

    public void LoadUpgradeButton(UpgradeButton upGradeButton)
    {
        string key = upGradeButton.UpgradeName;
        string GoldByUpgrade = upGradeButton.StartGoldByUpgrade.ToString();
        string CurrentCost = upGradeButton.StartCurrentCost.ToString();
        for (int i = 1; i <=20; i++)
        {
            upGradeButton.Level = PlayerPrefs.GetInt(key + "_Level", 0);
        }
        
        GoldByUpgrade = PlayerPrefs.GetString(key + "_goldByUpgrade", GoldByUpgrade);
        upGradeButton.goldByUpgrade = BigInteger.Parse(GoldByUpgrade);
        CurrentCost = PlayerPrefs.GetString(key + "+CurrentCost", CurrentCost);
        upGradeButton.CurrentCost = BigInteger.Parse(CurrentCost);
    }
    public void SaveUpgradeButton(UpgradeButton upGradeButton)
    {
        string key = upGradeButton.UpgradeName;
        
        PlayerPrefs.SetInt(key + "_Level", upGradeButton.Level);
        PlayerPrefs.SetString(key + "_goldByUpgrade", upGradeButton.goldByUpgrade.ToString());
        PlayerPrefs.SetString(key + "+CurrentCost", upGradeButton.CurrentCost.ToString());
    }
    public void Loaditem(ItemList itemlist) 
    {
        itemlist.item_Attack = PlayerPrefs.GetInt("itemAttack", itemlist.item_Attack);
        for (int i=0; i< itemlist.weaponData.dataArray.Length; i++)
        {
            string key = itemlist.weaponData.dataArray[i].UID;
            if(i == 0)
                itemlist.weaponData.dataArray[0].Level = PlayerPrefs.GetInt(key, 1);
            else
                itemlist.weaponData.dataArray[i].Level = PlayerPrefs.GetInt(key, 0);
        }
    }
    public void Saveitem(ItemList itemlist)
    {
        PlayerPrefs.SetInt("itemAttack", itemlist.item_Attack);
        for (int i = 0; i < itemlist.weaponData.dataArray.Length; i++)
        {
            string key = itemlist.weaponData.dataArray[i].UID;
            
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
        //mosterSpawn.stg.MonsterCount = PlayerPrefs.GetInt("MonsterCount", 1);

        string MonsterHP = mosterSpawn.MonsterHpCount.ToString();
        MonsterHP = PlayerPrefs.GetString("MonsterHpCount", MonsterHP);
        mosterSpawn.MonsterHpCount = BigInteger.Parse(MonsterHP);

        string BossHpCount = mosterSpawn.BossHpCount.ToString();
        BossHpCount = PlayerPrefs.GetString("BossHpCount", BossHpCount);
        mosterSpawn.BossHpCount = BigInteger.Parse(BossHpCount);
    }
    public void SaveStage(MonsterSpawn mosterSpawn)
    {
        PlayerPrefs.SetInt("Stage", (int)mosterSpawn.stg.curStage);
        //PlayerPrefs.SetInt("MonsterCount", (int)mosterSpawn.stg.MonsterCount);
        PlayerPrefs.SetString("MonsterHpCount", mosterSpawn.MonsterHpCount.ToString());
        PlayerPrefs.SetString("BossHpCount", mosterSpawn.BossHpCount.ToString());
    }
}




