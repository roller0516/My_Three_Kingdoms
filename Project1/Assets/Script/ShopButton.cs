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
    public int Cost;
    public bool PuchaseComplete;
    public ShopitemWeapon(string itemName, int Cost, bool isUsing, bool PuchaseComplete)
    {
        this.PuchaseComplete = PuchaseComplete;
        this.Cost = Cost;
        this.itemName = itemName;
        this.isUsing = isUsing;
    }
}
[System.Serializable]
public class ShopitemArmor
{
    public string itemName;
    public bool isUsing;
    public int Cost;
    public bool PuchaseComplete;
    public ShopitemArmor(string itemName,  int Cost,bool isUsing, bool PuchaseComplete)
    {
        this.PuchaseComplete = PuchaseComplete;
        this.Cost = Cost;
        this.itemName = itemName;
        this.isUsing = isUsing;
    }
}
public class ShopButton : MonoBehaviour
{
    public List<ShopitemWeapon> shopitemWeapon = new List<ShopitemWeapon>();
    public List<ShopitemArmor> shopitemAmor = new List<ShopitemArmor>();
    public Button[] AmorEquip;
    public Button[] WeaponEquip;
    int Weaponcount;
    int Amorcount;
    int Amortemp;
    int Weapontemp;
    public bool SkinOnWeapon;
    public bool SkinOnArmor;
    
    private void Start()
    {
        shopitemWeapon.Add(new ShopitemWeapon("짱돌", 200, false, false));
        shopitemWeapon.Add(new ShopitemWeapon("도검", 400, false, false));
        shopitemWeapon.Add(new ShopitemWeapon("사브르", 600, false, false));
        shopitemWeapon.Add(new ShopitemWeapon("레이 피어", 800, false, false));
        shopitemWeapon.Add(new ShopitemWeapon("광선검", 1000, false, false));
        shopitemAmor.Add(new ShopitemArmor("누더기 옷", 50, false, false));
        shopitemAmor.Add(new ShopitemArmor("평민 옷", 70, false, false));
        shopitemAmor.Add(new ShopitemArmor("상인 옷", 150, false, false));
        shopitemAmor.Add(new ShopitemArmor("사냥꾼 옷", 220, false, false));
        shopitemAmor.Add(new ShopitemArmor("철 갑옷", 320, false, false));
        shopitemAmor.Add(new ShopitemArmor("수호 갑옷", 470, false, false));
        shopitemAmor.Add(new ShopitemArmor("백호 갑옷", 560, false, false));
        shopitemAmor.Add(new ShopitemArmor("흑철 갑옷", 620, false, false));
        shopitemAmor.Add(new ShopitemArmor("황금 갑옷", 650, false, false));
        shopitemAmor.Add(new ShopitemArmor("용포", 700, false, false));
    }
    private void Update()
    {
        if (SkinOnWeapon == true)
            setSkin();
    }
    public void ChangeSkin_Amor(int num) 
    {
        
        if (shopitemAmor[num].PuchaseComplete == true)
        {
            if (Amortemp != num)
            {
                Amorcount = 1;
                if (Amorcount == 1)
                {
                    SkinOnArmor = true;
                    shopitemAmor[num].isUsing = true;
                    for (int i = 0; i < AmorEquip.Length; i++)
                    {
                        if (shopitemAmor[i].PuchaseComplete == true)
                        {
                            if (i == num)
                            {
                                AmorEquip[num].image.sprite = Resources.Load<Sprite>("UI/Shop/w_equipment");
                                continue;
                            }
                            AmorEquip[i].image.sprite = Resources.Load<Sprite>("UI/Shop/w_equip");
                        }

                    }
                    Player.Instance.skeletonAni.skeleton.SetSkin("animation/" + (num + 1));
                    Player.Instance.skeletonAni.skeleton.SetSlotsToSetupPose();
                    Amorcount++;
                }
            }
            else if(Amortemp == num)
            {
                print(Amorcount);
                if (Amorcount == 2)
                {
                    ReleaseAmorSkin(num);
                }
                else if (Amorcount == 1)
                {
                    SkinOnArmor = true;
                    shopitemAmor[num].isUsing = true;
                    for (int i = 0; i < AmorEquip.Length; i++)
                    {
                        if (shopitemAmor[i].PuchaseComplete == true)
                        {
                            if (i == num)
                            {
                                AmorEquip[num].image.sprite = Resources.Load<Sprite>("UI/Shop/w_equipment");
                                continue;
                            }
                            AmorEquip[i].image.sprite = Resources.Load<Sprite>("UI/Shop/w_equip");
                        }
                    }
                    Player.Instance.skeletonAni.skeleton.SetSkin("animation/" + (num + 1));
                    Player.Instance.skeletonAni.skeleton.SetSlotsToSetupPose();
                    Amorcount++;
                }
                Amortemp = num;
            }
        }
    }
    public void ChangeSkin_Weapon(int num)
    {

        SkinOnWeapon = true;
        shopitemWeapon[num].isUsing = true;


        if (Weaponcount == 1)
        {
            ReleaseWeaponSkin(num);
        }
        else if (Weaponcount == 0)
        {
            for (int i = 0; i < WeaponEquip.Length; i++)
            {
                shopitemWeapon[num].isUsing = false;
                if (i == num)
                {
                    WeaponEquip[num].image.sprite = Resources.Load<Sprite>("UI/Shop/w_equipment");
                    continue;
                }
                WeaponEquip[i].image.sprite = Resources.Load<Sprite>("UI/Shop/w_equip");
            }
            Player.Instance.skeletonRenderer.skeleton.SetAttachment("weapon", "weapon10" + (num + 1));
            Weaponcount++;
        }
       
    }
    public void puchaseshop(int num)
    {
        if (DataController.GetInstance().GetPaidGold() >= shopitemAmor[num].Cost && shopitemAmor[num].PuchaseComplete == false)
        {
            DataController.GetInstance().SubPaidGold(shopitemAmor[num].Cost);
            ChangeSkin_Amor(num);
            AmorEquip[num].image.sprite = Resources.Load<Sprite>("UI/Shop/w_equip");
            Amorcount = 1;
            shopitemAmor[num].PuchaseComplete = true;
        }
        else 
        {
            ChangeSkin_Amor(num);
        }
    }
    public void ReleaseAmorSkin(int num) 
    {
        SkinOnArmor = false;
        shopitemAmor[num].isUsing = false;
        Amorcount = 1;
        Player.Instance.skeletonRenderer.skeleton.SetSkin("animation/1");
        Player.Instance.skeletonAni.skeleton.SetSlotsToSetupPose();
        AmorEquip[num].image.sprite = Resources.Load<Sprite>("UI/Shop/w_equip");
    }
    public void ReleaseWeaponSkin(int num)
    {
        SkinOnWeapon = false;
        shopitemWeapon[num].isUsing = false;
        Weaponcount = 0;
        WeaponEquip[num].image.sprite = Resources.Load<Sprite>("UI/Shop/w_equip");
    }
    void setSkin() 
    {
        for (int i = 0; i< shopitemWeapon.Count;i++)
        {
            if(shopitemWeapon[i].isUsing== true)
                Player.Instance.skeletonRenderer.skeleton.SetAttachment("weapon", "weapon10" + (i + 1));
        }
    }
}
