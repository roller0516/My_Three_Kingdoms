using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UIManager : MonoBehaviour
{
    public GameObject weaponTap;

    public TextMeshProUGUI Gold;
    public TextMeshProUGUI[] GoldPerClickDisPlayer;
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
        GoldPerClickText(GoldPerClickDisPlayer);
        GoldText((float)DataController.GetInstance().GetGold());
       
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

    public void GoldText(float gold)
    {
        if (gold >= 100000) // c 십만
        {
            gold = gold / 100000;

            Gold.text = gold.ToString("0.00") + "c";
        }
        else if (gold >= 10000)// b 만
        {
            gold = gold / 10000;
            Gold.text = gold.ToString("0.00") + "b";
        }
        else if (gold >= 1000)// a 천
        {
            gold = gold / 1000;
            Gold.text = gold.ToString("0.00") + "a";
        }
        else if (gold < 1000)
        {
            Gold.text = gold.ToString("0");
        }
    }

    public void GoldPerClickText(TextMeshProUGUI[] txt)
    {
        
        txt = GoldPerClickDisPlayer;
       
        for (int i = 0;i< GoldPerClickDisPlayer.Length;i++)
        {
            float gold = (float)DataController.GetInstance().GetGoldPerClick("GoldperClick" + i);
            
            if (gold >= 100000)// c 십만
            {
                gold = gold/100000;

                txt[i].text = "+" + gold.ToString("0.00") + "c";
            }
            else if (gold >= 10000)// b 만
            {
                gold = gold/10000;

                txt[i].text = "+" + gold.ToString("0.00") + "b";
            }
            else if (gold >= 1000)// a 천
            {
                gold = gold/1000;

                txt[i].text = "+" + gold.ToString("0.00") + "a";

            }
            else if (gold < 1000)
            {
                txt[i].text = "+" + gold.ToString("0");

            }
        }
    }
    //public void WeaponCostText(TextMeshProUGUI[] txt)
    //{

    //    txt = GoldPerClickDisPlayer;

    //    for (int i = 0; i < GoldPerClickDisPlayer.Length; i++)
    //    {
    //        float gold = (float)DataController.GetInstance().GetGoldPerClick("GoldperClick" + i);

    //        if (gold >= 100000)// c 십만
    //        {
    //            gold = gold / 100000;

    //            txt[i].text = "+" + gold.ToString("0.00") + "c";
    //        }
    //        else if (gold >= 10000)// b 만
    //        {
    //            gold = gold / 10000;

    //            txt[i].text = "+" + gold.ToString("0.00") + "b";
    //        }
    //        else if (gold >= 1000)// a 천
    //        {
    //            gold = gold / 1000;

    //            txt[i].text = "+" + gold.ToString("0.00") + "a";

    //        }
    //        else if (gold < 1000)
    //        {
    //            txt[i].text = "+" + gold.ToString("0");

    //        }
    //    }
    //}
}
