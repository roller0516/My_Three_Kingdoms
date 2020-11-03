using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEngine.UI;
[System.Serializable]
public class ShopitemWeapon
{
    public string itemName;
    public bool isUsing;

    public ShopitemWeapon(string itemName, bool isUsing)
    {
        this.itemName = itemName;
        this.isUsing = isUsing;
    }
}
public class ShopButton : MonoBehaviour
{
    public List<ShopitemWeapon> shopitem = new List<ShopitemWeapon>();
    public Button[] AmorEquip;
    public Button[] WeaponEquip;
    int Weaponcount;
    int Amorcount;
    int Amortemp;
    int Weapontemp;
    public bool SkinOn;
    private void Start()
    {
        shopitem.Add(new ShopitemWeapon("짱돌", false));
        shopitem.Add(new ShopitemWeapon("도검", false));
        shopitem.Add(new ShopitemWeapon("사브르", false));
        shopitem.Add(new ShopitemWeapon("레이 피어", false));
        shopitem.Add(new ShopitemWeapon("광선검", false));
    }
    private void Update()
    {
        if (SkinOn == true)
            setSkin();
    }
    public void ChangeSkin_Amor(int num) 
    {
        if (Amortemp != num)
        {
            if (Amorcount == 1)
            {
                ReleaseAmorSkin(num);
            }
            if (Amorcount == 0)
            {
                print(Amortemp + "/" + num);
                for (int i = 0; i < AmorEquip.Length; i++)
                {
                    if (i == num)
                    {
                        AmorEquip[num].image.sprite = Resources.Load<Sprite>("UI/Shop/c_equipment");
                        continue;
                    }
                    AmorEquip[i].image.sprite = Resources.Load<Sprite>("UI/Shop/c_equip");
                }
                Player.Instance.skeletonAni.skeleton.SetSkin("animation/" + (num + 1));
                Player.Instance.skeletonAni.skeleton.SetSlotsToSetupPose();
                Amorcount++;
            }
        }
        else 
        {
            if (Amorcount == 1)
            {
                ReleaseAmorSkin(num);
            }
            else if (Amorcount == 0)
            {
                print(Amortemp + "/" + num);
                print(Amortemp);
                for (int i = 0; i < AmorEquip.Length; i++)
                {
                    if (i == num)
                    {
                        AmorEquip[num].image.sprite = Resources.Load<Sprite>("UI/Shop/c_equipment");
                        continue;
                    }
                    AmorEquip[i].image.sprite = Resources.Load<Sprite>("UI/Shop/c_equip");
                }
                Player.Instance.skeletonAni.skeleton.SetSkin("animation/" + (num + 1));
                Player.Instance.skeletonAni.skeleton.SetSlotsToSetupPose();
                Amorcount++;
            }
        }

        Amortemp = num;
    }
    public void ChangeSkin_Weapon(int num)
    {
        SkinOn = true;
        shopitem[num].isUsing = true;
        
        
        if (Weaponcount == 1)
        {
            ReleaseWeaponSkin(num);
        }
        else if (Weaponcount == 0)
        {
            for (int i = 0; i < WeaponEquip.Length; i++)
            {
                if (i == num)
                {
                    WeaponEquip[num].image.sprite = Resources.Load<Sprite>("UI/Shop/c_equipment");
                    continue;
                }
                WeaponEquip[i].image.sprite = Resources.Load<Sprite>("UI/Shop/c_equip");
            }
            Player.Instance.skeletonRenderer.skeleton.SetAttachment("weapon", "weapon10" + (num + 1));
            Weaponcount++;
        }
    }
    public void ReleaseAmorSkin(int num) 
    {
        Amorcount = 0;
        Player.Instance.skeletonRenderer.skeleton.SetSkin("animation/1");
        Player.Instance.skeletonAni.skeleton.SetSlotsToSetupPose();
        AmorEquip[num].image.sprite = Resources.Load<Sprite>("UI/Shop/c_equip");
    }
    public void ReleaseWeaponSkin(int num)
    {
        SkinOn = false;
        Weaponcount = 0;
        WeaponEquip[num].image.sprite = Resources.Load<Sprite>("UI/Shop/c_equip");
    }
    void setSkin() 
    {
        for (int i = 0; i< shopitem.Count;i++)
        {
            if(shopitem[i].isUsing== true)
                Player.Instance.skeletonRenderer.skeleton.SetAttachment("weapon", "weapon10" + (i + 1));
        }
    }
}
