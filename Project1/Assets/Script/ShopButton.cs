using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    Button Equip;
    int count;
    private void Start()
    {
        Equip = GetComponent<Button>();
    }
    public void ChangeSkin(int num) 
    {
        if (count == 1)
        {
            PrevSkin();
            return;
        }
        Player.Instance.skeletonRenderer.skeleton.SetSkin("animation/" + num);
        Equip.image.sprite =  Resources.Load<Sprite>("UI/Shop/c_equipment");
        count++;
    }
    void PrevSkin() 
    {
        count = 0;
        Player.Instance.skeletonRenderer.skeleton.SetSkin("animation/1");
        Equip.image.sprite = Resources.Load<Sprite>("UI/Shop/c_equip");
    }
}
