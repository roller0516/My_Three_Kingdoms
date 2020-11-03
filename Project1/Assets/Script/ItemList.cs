
using UnityEngine;
using UnityEngine.UI;
using System.Numerics;

public class ItemList : MonoBehaviour
{
    public string itemname;
    public weaponData weaponData;
    public Button[] bt;
    public Image[] im;
    public Slider[] WeaponGradeSlider;
    public BigInteger item_Attack;
    public int maxLevel = 10;
    ShopButton shop;
    private int StartAttackByUpgrade = 1;

    private void Start()
    {
        shop = FindObjectOfType<ShopButton>().GetComponent<ShopButton>();
        UpgradeWeapon(weaponData.dataArray[0].Level, 0);
        DataController.GetInstance().Loaditem(this);
        for (int i = 0; i < weaponData.dataArray.Length; i++)
            itemname = weaponData.dataArray[i].UID;
    }

    public void ButtonOn(string name)
    {
        for (int i = 0; i < weaponData.dataArray.Length; i++)
        {
            if (weaponData.dataArray[i].UID == name)//이름으로 찾는다
            {
                weaponData.dataArray[i].Isusing = true; //착용한상태로변경

                UpgradeWeapon(weaponData.dataArray[i].Level, i);
                if (i >= 1) // 나무막대기 이상의 급부터
                {
                    if (weaponData.dataArray[i].Isusing == true)
                        if (shop.SkinOn == false)
                            AttechmentPlayeritem(weaponData.dataArray[i].UID);
                    weaponData.dataArray[i-1].Isusing = false;
                }
                
            }
        }
        DataController.GetInstance().Saveitem(this);
    }
    public void Update()
    {
        if(shop.SkinOn == false)
            AttachmentCheck();
        WeaponUpGradeSlider();
    }

    public void UpgradeWeapon(int num,int num2)
    {
        switch (num)
        {
            case 1:
                item_Attack = BigInteger.Parse(weaponData.dataArray[num2].Atk);
                break;
            case 2:
                item_Attack = BigInteger.Parse(weaponData.dataArray[num2].Atk_2);
                break;
            case 3:
                item_Attack = BigInteger.Parse(weaponData.dataArray[num2].Atk_3);
                break;
            case 4:
                item_Attack = BigInteger.Parse(weaponData.dataArray[num2].Atk_4);
                break;
            case 5:
                item_Attack = BigInteger.Parse(weaponData.dataArray[num2].Atk_5);
                break;
            case 6:
                item_Attack = BigInteger.Parse(weaponData.dataArray[num2].Atk_6);
                break;
            case 7:
                item_Attack = BigInteger.Parse(weaponData.dataArray[num2].Atk_7);
                break;
            case 8:
                item_Attack = BigInteger.Parse(weaponData.dataArray[num2].Atk_8);
                break;
            case 9:
                item_Attack = BigInteger.Parse(weaponData.dataArray[num2].Atk_9);
                break;
            case 10:
                item_Attack = BigInteger.Parse(weaponData.dataArray[num2].Atk_10);
                break;
        }
    }
    public void AttechmentPlayeritem(string itemname)
    {
        Player.Instance.skeletonRenderer.skeleton.SetAttachment("weapon", itemname);
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
            WeaponGradeSlider[i].value = (float)weaponData.dataArray[i].Level / (float)maxLevel;
    }

}
