using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    public Button[] AmorEquip;
    public Button[] WeaponEquip;
    int count;

    public void ChangeSkin_Amor(int num) 
    {

        for (int i = 0; i < AmorEquip.Length; i++)
        {
            if (i == num)
            {
                AmorEquip[num].image.sprite = Resources.Load<Sprite>("UI/Shop/c_equipment");
                continue;
            }
            AmorEquip[i].image.sprite = Resources.Load<Sprite>("UI/Shop/c_equip");
        }
        Player.Instance.skeletonAni.skeleton.SetSkin("animation/"+(num+1));
        Player.Instance.skeletonAni.skeleton.SetSlotsToSetupPose();

        count++;
    }
    public void ChangeSkin_Weapon(int num)
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
        Player.Instance.skeletonRenderer.skeleton.SetAttachment("weapon", "weapon10"+(num+1));
        print("무기변경");
    }
    void PrevSkin(int num) 
    {
        count = 0;
        Player.Instance.skeletonRenderer.skeleton.SetSkin("animation/1");
        WeaponEquip[num].image.sprite = Resources.Load<Sprite>("UI/Shop/c_equip");
    }
}
