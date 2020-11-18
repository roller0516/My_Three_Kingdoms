using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEngine.UI;
using TMPro;
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
    public TextMeshProUGUI[] GoldWeapnText;
    public TextMeshProUGUI[] GoldArmorText;
    public Image[] Gold_im_weapon;
    public Image[] Gold_im_Armor;
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

        shopitemAmor.Add(new ShopitemArmor("누더기 옷", 0, true, true));
        shopitemAmor.Add(new ShopitemArmor("평민 옷", 70, false, false));
        shopitemAmor.Add(new ShopitemArmor("상인 옷", 150, false, false));
        shopitemAmor.Add(new ShopitemArmor("사냥꾼 옷", 220, false, false));
        shopitemAmor.Add(new ShopitemArmor("철 갑옷", 320, false, false));
        shopitemAmor.Add(new ShopitemArmor("수호 갑옷", 470, false, false));
        shopitemAmor.Add(new ShopitemArmor("백호 갑옷", 560, false, false));
        shopitemAmor.Add(new ShopitemArmor("흑철 갑옷", 620, false, false));
        shopitemAmor.Add(new ShopitemArmor("황금 갑옷", 650, false, false));
        shopitemAmor.Add(new ShopitemArmor("용포", 700, false, false));
        DataController.GetInstance().LoadShop(this);

        for (int i = 0; i < shopitemWeapon.Count; i++)
        {
            GoldWeapnText[i].text = shopitemWeapon[i].Cost.ToString();

            if (shopitemWeapon[i].PuchaseComplete == true && shopitemWeapon[i].isUsing == true)
            {
                WeaponEquip[i].image.sprite = Resources.Load<Sprite>("UI/Shop/w_equipment");
                Weaponcount = 2;
                Weapontemp = i;
                GoldWeapnText[i].gameObject.SetActive(false);
                Gold_im_weapon[i].gameObject.SetActive(false);
            }

            else if (shopitemWeapon[i].PuchaseComplete == true) 
            {
                GoldWeapnText[i].gameObject.SetActive(false);
                Gold_im_weapon[i].gameObject.SetActive(false);
                WeaponEquip[i].image.sprite = Resources.Load<Sprite>("UI/Shop/w_equip");
            }
        }
        for (int i = 0; i < shopitemAmor.Count; i++)
        {
            GoldArmorText[i].text = shopitemAmor[i].Cost.ToString();
            if (shopitemAmor[i].PuchaseComplete == true && shopitemAmor[i].isUsing == true)
            {
                Player.Instance.skeletonAni.skeleton.SetSkin("animation/" + (i + 1));
                Player.Instance.skeletonAni.skeleton.SetSlotsToSetupPose();

                AmorEquip[i].image.sprite = Resources.Load<Sprite>("UI/Shop/w_equipment");
                Amorcount = 2;
                Amortemp = i;
                GoldArmorText[i].gameObject.SetActive(false);
                Gold_im_Armor[i].gameObject.SetActive(false);
            }

            else if (shopitemAmor[i].PuchaseComplete == true) 
            {
                GoldArmorText[i].gameObject.SetActive(false);
                Gold_im_Armor[i].gameObject.SetActive(false);
                AmorEquip[i].image.sprite = Resources.Load<Sprite>("UI/Shop/w_equip");
            }
                
        }
       
    }
    private void Update()
    {
        if (SkinOnWeapon == true)
            setSkin();
        for (int i = 0; i < shopitemWeapon.Count;i++) 
        {
            Weapon_textColor(i);
        }
        for (int i = 0; i < shopitemAmor.Count; i++) 
        {
            Armor_textColor(i);
        }
    }
    public void ChangeSkin_Amor(int num) 
    {
        
        if (shopitemAmor[num].PuchaseComplete == true)
        {
            GoldArmorText[num].gameObject.SetActive(false);
            Gold_im_Armor[num].gameObject.SetActive(false);
            if (Amortemp != num)
            {
                Amorcount = 1;
                shopitemAmor[Amortemp].isUsing = false;
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
            }
            Amortemp = num;
        }
        DataController.GetInstance().SaveShop(this);
    }
    public void ChangeSkin_Weapon(int num)
    {
        if (shopitemWeapon[num].PuchaseComplete == true)
        {
            GoldWeapnText[num].gameObject.SetActive(false);
            Gold_im_weapon[num].gameObject.SetActive(false);
            if (Weapontemp != num)
            {
                Weaponcount = 1;
                shopitemWeapon[Weapontemp].isUsing = false;
            }
                
            if (Weaponcount == 2)
            {
                ReleaseWeaponSkin(num);
            }
            else if (Weaponcount == 1)
            {
                SkinOnWeapon = true;
                shopitemWeapon[num].isUsing = true;
                for (int i = 0; i < WeaponEquip.Length; i++)
                {
                    if (shopitemWeapon[i].PuchaseComplete == true)
                    {
                        if (i == num)
                        {
                            WeaponEquip[num].image.sprite = Resources.Load<Sprite>("UI/Shop/w_equipment");
                            continue;
                        }
                        WeaponEquip[i].image.sprite = Resources.Load<Sprite>("UI/Shop/w_equip");
                    }
                }
                Player.Instance.skeletonRenderer.skeleton.SetAttachment("weapon", "weapon10" + (num + 1));
                Weaponcount++;
            }
            Weapontemp = num;
            DataController.GetInstance().SaveShop(this);
        }
    }
    public void puchaseArmor(int num)
    {
        
        if (DataController.GetInstance().GetPaidGold() >= shopitemAmor[num].Cost && shopitemAmor[num].PuchaseComplete == false)
        {
            SoundManager.instance.Purchase();
            DataController.GetInstance().SubPaidGold(shopitemAmor[num].Cost);
            ChangeSkin_Amor(num);
            AmorEquip[num].image.sprite = Resources.Load<Sprite>("UI/Shop/w_equip");
            Amorcount = 1;
            shopitemAmor[num].PuchaseComplete = true;
            GoldArmorText[num].gameObject.SetActive(false);
            Gold_im_Armor[num].gameObject.SetActive(false);
        }
        else 
        {
            SoundManager.instance.EquipSound();
            ChangeSkin_Amor(num);
        }
    }
    public void puchaseWeapon(int num)
    {
        
        if (DataController.GetInstance().GetPaidGold() >= shopitemWeapon[num].Cost && shopitemWeapon[num].PuchaseComplete == false)
        {
            SoundManager.instance.Purchase();
            DataController.GetInstance().SubPaidGold(shopitemWeapon[num].Cost);
            ChangeSkin_Weapon(num);
            WeaponEquip[num].image.sprite = Resources.Load<Sprite>("UI/Shop/w_equip");
            Weaponcount = 1;
            shopitemWeapon[num].PuchaseComplete = true;
            GoldWeapnText[num].gameObject.SetActive(false);
            Gold_im_weapon[num].gameObject.SetActive(false);
        }
        else
        {
            SoundManager.instance.EquipSound();
            ChangeSkin_Weapon(num);
            Amorcount = 1;
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
        Weaponcount = 1;
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
    public void Armor_textColor(int num)//재화 부족시 컬러변경
    {

         if (DataController.GetInstance().GetPaidGold() < shopitemAmor[num].Cost)
         {
            GoldArmorText[num].color = Color.red;
         }
         else
         {
            GoldArmorText[num].color = Color.yellow;
         }
    }
    public void Weapon_textColor(int num)//재화 부족시 컬러변경
    {

        if (DataController.GetInstance().GetPaidGold() < shopitemWeapon[num].Cost)
        {
            GoldWeapnText[num].color = Color.red;
        }
        else
        {
            GoldWeapnText[num].color = Color.yellow;
        }
    }
}
