using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI Gold;
    public TextMeshProUGUI[] GoldPerClickDisPlayer;
    public UpgradeButton[] UpgradeButton;
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
        GoldPerClickText(GoldPerClickDisPlayer);
        GoldText(DataController.GetInstance().GetGold());
    }

    public void GoldText(float gold)
    {
        if (gold >= 100000) // B 십만
        {
            gold = gold / 100000;

            Gold.text = gold.ToString("0.00") + "b";
        }
        else if (gold >= 10000)// A 만
        {
            gold = gold / 10000;
            Gold.text = gold.ToString("0.00") + "a";
        }
        else if (gold < 10000)
        {
            Gold.text = gold.ToString("0");
        }
    }

    public void GoldPerClickText(TextMeshProUGUI[] txt)
    {
        
        txt = GoldPerClickDisPlayer;
       
        for (int i = 0;i< GoldPerClickDisPlayer.Length;i++)
        {
            int gold = DataController.GetInstance().GetGoldPerClick("GoldperClick" + i);
            
            if (gold >= 100000)// B 십만
            {
                gold = gold / 100000;
                txt[i].text = "+" + gold.ToString("0.00") + "b";
                
            }
            else if (gold >= 10000)// A 만
            {
                gold = gold / 10000;
                txt[i].text = "+" + gold.ToString("0.00") + "a";
            }
            else if (gold >= 1000)
            {
                txt[i].text = "+" + gold.ToString("0");

            }
            else if (gold < 1000)
            {
                txt[i].text = "+" + gold.ToString("0");

            }
        }
        
    }


}
