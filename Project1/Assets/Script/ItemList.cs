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
    public int item_Attack;
    public int AttackUpgrade;

    private int StartAttackByUpgrade = 1;
    private int maxLevel = 10;
    
    
    private void Start()
    {
        AllitemList.Add(new Item("Onehand01",1, 1, true));
        AllitemList.Add(new Item("Sword01", 10, 0, false));
        AllitemList.Add(new Item("Spear01", 20, 0, false));
        AllitemList.Add(new Item("녹슨검", 30, 0, false));
        AllitemList.Add(new Item("녹슨검1", 40, 0, false));
        AllitemList.Add(new Item("녹슨검2", 50, 0, false));
        AllitemList.Add(new Item("녹슨검3", 60, 0, false));
        AllitemList.Add(new Item("녹슨검4", 70, 0, false));
        AllitemList.Add(new Item("녹슨검5", 80, 0, false));
        AllitemList.Add(new Item("녹슨검6", 90, 0, false));

        item_Attack = AllitemList[0].Attack;
        DataController.GetInstance().LoadUpgradeButton(this);
    }
   
    public void ButtonOn(string name)
    {
        itemname = name;
        for (int i = 0; i<AllitemList.Count;i++)
        {
           
            if (AllitemList[i].name == itemname)//이름으로 찾는다
            {

                AllitemList[i].isUsing = true; //착용한상태로변경
                AllitemList[i].level++; //레벨을 올려주고 
                //강화칸을 증가시켜줌
                                                                                           // 업그레이드 공식
                
                if (i >= 1) // 나무막대기 이상의 급부터
                {
                    if (AllitemList[i].isUsing == true)
                        AttechmentPlayeritem(AllitemList[i].name);
                    AllitemList[i - 1].isUsing = false;
                }

                UpgradeWeapon();
                item_Attack = AllitemList[i].Attack + AttackUpgrade;
            }
        }

        DataController.GetInstance().SaveUpgradeButton(this);
    }
    public void Update()
    {
        UpgradeCount();
        AttachmentCheck();
        WeaponUpGradeSlider();
    }

    public void UpgradeWeapon()
    {
        AttackUpgrade = StartAttackByUpgrade + AttackUpgrade;
    }
    public void UpgradeCount()// 아이템갯수만큼 돌면서 level이 levelmax가 되는지 체크한다.
    {
        for (int i = 0; i < AllitemList.Count; i++) 
        {
            if (AllitemList[i].level < maxLevel && AllitemList[i].level > 0) // 모든 아이템의 레벨이 0보다크고 맥스치보단 작다면
            {
                bt[i].interactable = true;//버튼 활성화
            }
            else if (AllitemList[i].level >= maxLevel)
            {
                bt[i].interactable = false;

                if (i == AllitemList.Count - 1)// i가 마지막일때는 return으로 빠져나간다.
                    return;
                else if (AllitemList[i + 1].level == 0)
                    bt[i + 1].interactable = true;
            }
        }
    }
    public void AttechmentPlayeritem(string itemname) 
    {
        Player.Instance.skeletonRenderer.skeleton.SetAttachment("weapon", itemname);
    }
    public void AttachmentCheck() 
    {
        for (int i = 0; i < AllitemList.Count; i++)
            if (AllitemList[i].isUsing == true)
                AttechmentPlayeritem(AllitemList[i].name);
    }
    public void WeaponUpGradeSlider() 
    {
        for (int i = 0; i < AllitemList.Count; i++)
        {
            WeaponGradeSlider[i].value = (float)AllitemList[i].level / (float)maxLevel;
        }
    }
}
