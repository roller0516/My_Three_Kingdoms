using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;
[System.Serializable]
public class Specialitem
{
    public string itemName = "";
    public int itemCount;
    public int AbilityCount;
    public bool AbiliOn;
    public Specialitem(string _itemName, int _itemCount,int _AbilityCount,bool _AbiliOn)
    {
        this.itemName = _itemName;
        this.itemCount = _itemCount;
        this.AbilityCount = _AbilityCount;
        this.AbiliOn = _AbiliOn;
    }
}
public class SpecialitemList : MonoBehaviour
{
    public List<Specialitem> Sp_item = new List<Specialitem>();
    int[] ItemCount;
    bool isUsing = false;
    int[] Count;
    private void Start()
    {
        Additem("쌍철극", 0, 30, false);
        Additem("의천검", 0, 50, false);
        Additem("칠성검", 0, 100, false);
        Additem("유성추", 0, 200, false);
        Additem("맹획머리띠", 0, 200, false);
        Additem("병법24편", 0, 30, false);
        Additem("비도", 0, 300, false);
        Additem("적로", 0, 100, false);
        Additem("조황비전", 0, 100, false);
        Additem("단극", 0, 20, false);
        Additem("청공검", 0, 100, false);
        Additem("백염부", 0, 100, false);
        Additem("방천화극", 0, 5, false);
        Additem("적토마", 0, 15, false);
        Additem("옥새", 0, 50, false);
        Additem("고정도", 0, 50, false); 

        ItemCount = new int[Sp_item.Count];
        Count = new int[Sp_item.Count];
    }

    public void AbilityOn(int num)
    {
        Sp_item[num].AbiliOn = true;

        if (Sp_item[num].AbiliOn == true && Count[num] == 0)
        {
            print(Sp_item[num].itemName);
            switch (Sp_item[num].itemName)
            {
                case "쌍철극":
                    Count[num]++;
                    DataController.GetInstance().Teasure2Ability += Sp_item[num].AbilityCount;
                    for (int i =0; i < UIManager.GetInstance().upgradeButton.Length;i++)
                    {
                        UIManager.GetInstance().upgradeButton[i].UpgradeTik();
                    }
                    Sp_item[num].AbiliOn = false;
                    break;
                case "의천검":
                    Count[num]++;
                    DataController.GetInstance().Teasure1Ability += Sp_item[num].AbilityCount;
                    for (int i = 0; i < UIManager.GetInstance().upgradeButton.Length; i++)
                    {
                        UIManager.GetInstance().upgradeButton[i].UpgradeTik();
                    }
                    Sp_item[num].AbiliOn = false;
                    break;
                case "칠성검":
                    //UIManager.GetInstance().Teasurecost_Nomal[1].goldByUpgrade += Sp_item[i].AbilityCount;
                    break;
                case "유성추":
                    //UIManager.GetInstance().Teasurecost_Nomal[1].goldByUpgrade += Sp_item[i].AbilityCount;
                    break;
                case "맹획머리띠":
                    print("성공");
                    Count[num]++;
                    Player.Instance.CriticalPer += Sp_item[num].AbilityCount;
                    Sp_item[num].AbiliOn = false;
                    break;
                case "병법24편":
                    Count[num]++;
                    Player.Instance.Critical += Sp_item[num].AbilityCount;
                    Sp_item[num].AbiliOn = false;
                    break;
                case "비도":
                    break;
                case "적로":
                    Count[num]++;
                    Player.Instance.my_PlayerDamage += Player.Instance.my_PlayerDamage;
                    Sp_item[num].AbiliOn = false;
                    break;
                case "조황비전":
                    break;
                case "단극":
                    break;
                case "청공검":
                    Count[num]++;
                    Player.Instance.my_PlayerDamage += Player.Instance.my_PlayerDamage;
                    Sp_item[num].AbiliOn = false;
                    break;
                case "백염부":
                    Count[num]++;
                    Player.Instance.my_PlayerDamage += Player.Instance.my_PlayerDamage;
                    Sp_item[num].AbiliOn = false;
                    break;
                case "방천화극":
                    Count[num]++;
                    DataController.GetInstance().Teasure2Ability += Sp_item[num].AbilityCount;
                    Sp_item[num].AbiliOn = false;
                    break;
                case "적토마":
                    Count[num]++;
                    DataController.GetInstance().Teasure2Ability += Sp_item[num].AbilityCount;
                    Sp_item[num].AbiliOn = false;
                    break;
                case "옥새":
                    break;
                case "고정도":
                    Count[num]++;
                    DataController.GetInstance().Teasure1Ability += Sp_item[num].AbilityCount;
                    Sp_item[num].AbiliOn = false;
                    break;
            }
        }
        
    }

    public void Additem(string itemName, int itemCount, int AbilityCount,bool AbiliOn)
    {
        Sp_item.Add(new Specialitem(itemName, itemCount, AbilityCount, AbiliOn));
    }
    private void Update()
    {
        for (int i = 0; i < Sp_item.Count; i++)
        {
            ItemCount[i] = Sp_item[i].itemCount;
            if (ItemCount[i] == 10)
            {
                isUsing = true;
                AbilityOn(i);
            }
               
        }
    }
}
