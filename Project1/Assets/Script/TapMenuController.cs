using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class TapMenuController : MonoBehaviour
{
    public Animator ani;

    private void Start()
    {
        ani = GetComponent<Animator>(); // 시작하면 훈련탭이 열리는 애니메이션
        ani.SetTrigger("Training");
    }

    public void TrainingOnActive() // 애니메이션 훈련탭으로 전환
    {
        SoundManager.instance.TapSound();
        ani.SetTrigger("Training");
        if (UIManager.GetInstance().SearchTap.activeSelf == true)
            PopUpSystem.GetInstance().ClosePopUp();
    }

    public void WeaponOnActive()//애니메이션 무기탭으로 전환
    {
        SoundManager.instance.TapSound();
        ani.SetTrigger("Weapon");
        if (UIManager.GetInstance().SearchTap.activeSelf == true)
            PopUpSystem.GetInstance().ClosePopUp();

    }
   
    public void TeasureOnActive()//애니메이션 보물탭으로 전환
    {
        SoundManager.instance.TapSound();
        ani.SetTrigger("Teasure");
        if (UIManager.GetInstance().SearchTap.activeSelf == true)
            PopUpSystem.GetInstance().ClosePopUp();
    }

    public void ShopOnActive()//애니메이션 상점탭으로 전환
    {
        SoundManager.instance.TapSound();
        ani.SetTrigger("Shop");
        if(UIManager.GetInstance().SearchTap.activeSelf == true)
            PopUpSystem.GetInstance().ClosePopUp();

    }

    public void SearchOnActive()//애니메이션 수색탭으로 전환
    {
        SoundManager.instance.TapSound();
        ani.SetTrigger("Search");
        
    }
    public void DictionaryOnActive()//애니메이션 수색탭으로 전환
    {
        SoundManager.instance.TapSound();
        ani.SetTrigger("Dictionary");
        if (UIManager.GetInstance().SearchTap.activeSelf == true)
            PopUpSystem.GetInstance().ClosePopUp();
    }

    public void TeasureNomalButton()
    {
        SoundManager.instance.TapSound();
        ani.SetTrigger("TeasureNomal");
    }
    public void TeasureSpecialButton()
    {
        SoundManager.instance.TapSound();
        ani.SetTrigger("TeasureSpecial");
    }
    public void ShopClothes()
    {
        SoundManager.instance.TapSound();
        ani.SetTrigger("ShopClothes");
    }
    public void ShopWeapon()
    {
        SoundManager.instance.TapSound();
        ani.SetTrigger("ShopWeapon");
    }
}
