using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text GoldDisPlayer;
    public Text[] LevelTex;
    public Text[] upGradeTex;

    UpgradeButton UpgradeButton;
    private void Awake()
    {
        UpgradeButton = GameObject.Find("Button1").GetComponent<UpgradeButton>();
    }
    private void Update()
    {
        
        GoldDisPlayer.text = "" + DataController.GetInstance().GetGold();
    }
    public void UpdateUI(int num_)
    {
        
        switch (num_)
        {
            case 1:
                LevelTex[0].text = "Lv" + UpgradeButton.Level.ToString();

                upGradeTex[0].text = "" + UpgradeButton.CurrentCost;

                print("1로들어옴");
                break;
            case 2:
                LevelTex[1].text = "Lv" + UpgradeButton.Level2.ToString();

                upGradeTex[1].text = "" + UpgradeButton.CurrentCost;

                print("2로들어옴");
                break;

        }
    }
}
