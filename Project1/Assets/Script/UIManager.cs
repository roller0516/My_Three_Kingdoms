using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI GoldDisPlayer;
    [HideInInspector]
    public int[] Level;
    
    public UpgradeButton[] UpgradeButton;
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
        GoldText(DataController.GetInstance().GetGold());
    }
    public void GoldText(float gold)
    {
        if (gold >= 100000) // B 십만
        {
            gold = gold / 100000;
            
            GoldDisPlayer.text = gold.ToString("0.00") + "B";
        }
        else if (gold >= 10000)// A 만
        {
            gold = gold / 10000;
            GoldDisPlayer.text = gold.ToString("0.00") + "A";
        }
        else if (gold < 10000)
        {
            GoldDisPlayer.text = gold.ToString("0");
        }
    }
    
   
}
