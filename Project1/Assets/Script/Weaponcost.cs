using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Numerics;

public class Weaponcost : MonoBehaviour
{
    public string UpgradeName;
    public string increase;
    [HideInInspector]
    public BigInteger CurrentCost;
    [HideInInspector]
    public int goldByUpgrade;
    public string StartCost;
    [HideInInspector]
    public int MaxLevel = 10;
    public TextMeshProUGUI upGradeTex;
    public Image im;
    ItemList item_l;
    public ParticleSystem particle;


    private void Start()
    {
        CurrentCost = BigInteger.Parse( StartCost);
        item_l = FindObjectOfType<ItemList>().GetComponent<ItemList>();
        DataController.GetInstance().LoadWeaponCost(this);
        UpdateUI();
    }

    public void PurChaseUpgrade(int num) //구매 함수
    {
        if (item_l.weaponData.dataArray[num].Level < MaxLevel)
        {
            SoundManager.instance.WeaponButtonSound();
            if (DataController.GetInstance().GetGold() >= CurrentCost)
            {
                
                StartCoroutine("StartParticle");
                DataController.GetInstance().SubGold(CurrentCost);
                
                UpdateUpgrade(num);

                
                UpdateUI();
                item_l.weaponData.dataArray[num].Level++;
            }
        }
        DataController.GetInstance().SaveWeaponCost(this);
    }

    public void UpdateUpgrade(int num) // 업그레이드 공식 
    {
       switch (num)
       {
           case 0:
               CurrentCost += BigInteger.Parse(increase); 
               break;
           case 1:
                CurrentCost += BigInteger.Parse(increase);
               break;
           case 2:
                CurrentCost += BigInteger.Parse(increase);
               break;
           case 3:
                CurrentCost += BigInteger.Parse(increase);
               break;
           case 4:
                CurrentCost += BigInteger.Parse(increase);
               break;
           case 5:
                CurrentCost += BigInteger.Parse(increase);
               break;
           case 6:
                CurrentCost += BigInteger.Parse(increase);
               break;
           case 7:
                CurrentCost += BigInteger.Parse(increase);
               break;
           case 8:
               CurrentCost += BigInteger.Parse(increase);
               break;
           case 9:
               CurrentCost += BigInteger.Parse(increase);
               break;
           case 10:
               CurrentCost += BigInteger.Parse(increase);
               break;
           case 11:
               CurrentCost += BigInteger.Parse(increase);
               break;
           case 12:
               CurrentCost += BigInteger.Parse(increase);
               break;
           case 13:
               CurrentCost += BigInteger.Parse(increase);
               break;
           case 14:
               CurrentCost += BigInteger.Parse(increase);
               break;
           case 15:
               CurrentCost += BigInteger.Parse(increase);
               break;
           case 16:
               CurrentCost += BigInteger.Parse(increase);
               break;
           case 17:
                CurrentCost += BigInteger.Parse(increase);
               break;
           case 18:
               CurrentCost += BigInteger.Parse(increase);
               break;
           case 19:
                CurrentCost += BigInteger.Parse(increase);
                break;
        }
    }

    public void UpdateUI()//ui의 변화를 받아온다
    {
        upGradeTex.text = "" + CurrentCost;
    }

    private void Update()
    {
        ScarceCost_textColor();
    }

    public void ScarceCost_textColor()//재화 부족시 컬러변경
    {
        for (int i = 0; i < item_l.weaponData.dataArray.Length; i ++)
        {
            if (item_l.weaponData.dataArray[i].Level != MaxLevel)
            {
                if (DataController.GetInstance().GetGold() < CurrentCost)
                {
                    upGradeTex.color = Color.red;
                }
                else
                {
                    upGradeTex.color = Color.yellow;
                }
            }
            
        }
    }
    IEnumerator StartParticle() 
    {
        particle.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        particle.Play();
        yield return new WaitForSeconds(0.1f);
        particle.gameObject.SetActive(false);
    }
}
