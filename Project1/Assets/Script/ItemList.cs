
using UnityEngine;
using UnityEngine.UI;

public class ItemList : MonoBehaviour
{
    private static ItemList s_instance = null;

    public static ItemList Instance
    {
        get
        {
            if (s_instance == null)
            {
                s_instance = FindObjectOfType(typeof(ItemList)) as ItemList;
            }
            return s_instance;
        }
    }
    public weaponData weaponData;
    public Button[] bt;
    public Image[] im;
    public Slider[] WeaponGradeSlider;
    public string itemname;
    public int item_Attack;

    private int StartAttackByUpgrade = 1;
    public int maxLevel = 10;
    
    
    private void Start()
    {
        UpgradeWeapon(weaponData.dataArray[0].Level, 0);
        DataController.GetInstance().Loaditem(this);
    }

    public void ButtonOn(string name)
    {
        itemname = name;
        for (int i = 0; i < weaponData.dataArray.Length; i++)
        {
            if (weaponData.dataArray[i].UID == name)//이름으로 찾는다
            {
                weaponData.dataArray[i].Isusing = true; //착용한상태로변경
                //레벨을 올려주고 

                UpgradeWeapon(weaponData.dataArray[i].Level + 1, i);

                if (i >= 1) // 나무막대기 이상의 급부터
                {
                    if (weaponData.dataArray[i].Isusing == true)
                        AttechmentPlayeritem(weaponData.dataArray[i].UID);
                    weaponData.dataArray[i-1].Isusing = false;
                }
            }
        }
        DataController.GetInstance().Saveitem(this);
    }
    public void Update()
    {
        AttachmentCheck();
        WeaponUpGradeSlider();
    }

    public void UpgradeWeapon(int num,int num2)
    {
        switch (num)
        {
            case 1:
                item_Attack = weaponData.dataArray[num2].Atk;
                break;
            case 2:
                item_Attack = weaponData.dataArray[num2].Atk_2;
                break;
            case 3:
                item_Attack = weaponData.dataArray[num2].Atk_3;
                break;
            case 4:
                item_Attack = weaponData.dataArray[num2].Atk_4;
                break;
            case 5:
                item_Attack = weaponData.dataArray[num2].Atk_5;
                break;
            case 6:
                item_Attack = weaponData.dataArray[num2].Atk_6;
                break;
            case 7:
                item_Attack = weaponData.dataArray[num2].Atk_7;
                break;
            case 8:
                item_Attack = weaponData.dataArray[num2].Atk_8;
                break;
            case 9:
                item_Attack = weaponData.dataArray[num2].Atk_9;
                break;
            case 10:
                item_Attack = weaponData.dataArray[num2].Atk_10;
                break;
        }
        
    }
   
    public void AttechmentPlayeritem(string itemname)
    {
        Player.Instance.skeletonRenderer.skeleton.SetAttachment("weapon", "Spear01");
    }
    public void AttachmentCheck()
    {
        for (int i = 0; i < weaponData.dataArray.Length; i++)
            if (weaponData.dataArray[i].Isusing == true)
                AttechmentPlayeritem(weaponData.dataArray[i].UID);
    }
    public void WeaponUpGradeSlider()
    {
        for (int i = 0; i < weaponData.dataArray.Length; i++)
        {
            WeaponGradeSlider[i].value = (float)weaponData.dataArray[i].Level / (float)maxLevel;
        }
    }
}
