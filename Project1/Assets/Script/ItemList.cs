using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class Item
{
    public Item(string _name, int _Attack, int _level,bool _isUsing)
    {
        name = _name;
        Attack = _Attack;
        level = _level;
        isUsing = _isUsing;
    }
    public string name;
    public int Attack, level;
    public bool isUsing;
}
public class ItemList : MonoBehaviour
{
    public List<Item> AllitemList;
    public Button[] bt;
    public Slider[] WeaponGradeSlider;
    public string itemname;
    public int _Attack;
    public int level;

    private int StartAttackByUpgrade = 1;
    private int AttackUpgrade;
    private int maxLevel = 10;
    
    
    private void Start()
    {
        AllitemList.Add(new Item("나무막대기",1, 0, true));
        AllitemList.Add(new Item("Onehand01", 10, 0, false));
        AllitemList.Add(new Item("Spear01", 20, 0, false));
        AllitemList.Add(new Item("녹슨검", 30, 0, false));
        AllitemList.Add(new Item("녹슨검1", 40, 0, false));
        AllitemList.Add(new Item("녹슨검2", 50, 0, false));
        AllitemList.Add(new Item("녹슨검3", 60, 0, false));
        AllitemList.Add(new Item("녹슨검4", 70, 0, false));
        AllitemList.Add(new Item("녹슨검5", 80, 0, false));
        AllitemList.Add(new Item("녹슨검6", 90, 0, false));

        DataController.GetInstance().LoadUpgradeButton(this);
        
    }
   
    public void ButtonOn(string name)
    {
        itemname = name;
        for (int i = 0; i<AllitemList.Count;i++)
        {
            if (AllitemList[i].name == itemname)
            {
                AllitemList[i].isUsing = true;
                AllitemList[i].level++;
                WeaponGradeSlider[i].value = (float)AllitemList[i].level / (float)maxLevel;
                bt[i].interactable = true;
                UpgradeWeapon();
                
                if (i >= 1)
                {
                    AllitemList[i - 1].isUsing = false;
                    bt[i - 1].interactable = false;
                    if(AllitemList[i].isUsing == true)
                        Player.Instance.skeletonRenderer.skeleton.SetAttachment("weapon", "Spear01");
                }
                if (AllitemList[i].level >= maxLevel)
                {
                    bt[i].interactable = false;
                    if (i + 1 < AllitemList.Count)
                        bt[i + 1].interactable = true;
                }
                _Attack = AllitemList[i].Attack + AttackUpgrade;

                DataController.GetInstance().SaveUpgradeButton(this);
            }
        }
    }
    public void Update()
    {
        UpgradeCount();
    }

    public void UpgradeWeapon()
    {
        AttackUpgrade = StartAttackByUpgrade + AttackUpgrade;
    }
    public void UpgradeCount()
    {
        for (int i = 0; i < AllitemList.Count; i++)
        {
            if (AllitemList[i].level >= maxLevel)
            {
                bt[i].interactable = false;
                if (AllitemList[i + 1].level == 0)
                    bt[i + 1].interactable = true;
            }
            else if (AllitemList[i].level < maxLevel && AllitemList[i].level > 0)
            {
                bt[i].interactable = true;
            }
        }
    }
        
}
