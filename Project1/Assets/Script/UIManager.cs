using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text GoldDisPlayer;
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
        GoldDisPlayer.text = "" + DataController.GetInstance().GetGold();
    }
    
}
