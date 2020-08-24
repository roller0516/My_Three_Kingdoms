using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private void Awake()
    {
        m_gold = PlayerPrefs.GetInt("Gold");
        m_goldperClick = PlayerPrefs.GetInt("GoldPerClick",1);
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
    public int GetGoldPerClick()
    {
        return m_goldperClick;
    }
    public void SetGoldPerClick(int newGoldPerClick)
    {
        m_goldperClick = newGoldPerClick;
        PlayerPrefs.SetInt("GoldPerClick", m_goldperClick);
    }
    public void AddGoldPerClick(int newGoldPerClick)
    {
        m_goldperClick += newGoldPerClick;
        SetGoldPerClick(m_goldperClick); 
    }
    public void LoadUpgradeButton(UpgradeButton upGradeButton)
    {
        string key = upGradeButton.UpgradeName;

        upGradeButton.Level = PlayerPrefs.GetInt(key + "_Level",1);
        upGradeButton.goldByUpgrade = PlayerPrefs.GetInt(key + "_goldByUpgrade", upGradeButton.StartGoldByUpgrade);
        upGradeButton.CurrentCost = PlayerPrefs.GetInt(key + "+CurrentCost", upGradeButton.StartCurrentCost);

    }
    public void SaveUpgradeButton(UpgradeButton upGradeButton)
    {
        string key = upGradeButton.UpgradeName;

        PlayerPrefs.SetInt(key + "_Level",upGradeButton.Level);
        PlayerPrefs.SetInt(key + "_goldByUpgrade", upGradeButton.goldByUpgrade);
        PlayerPrefs.SetInt(key + "+CurrentCost", upGradeButton.CurrentCost);
    }
}



// Update is called once per frame
