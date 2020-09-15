using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class TapMenuController : MonoBehaviour
{
    Animator ani;


    private void Start()
    {
        ani = GetComponent<Animator>(); // 시작하면 훈련탭이 열리는 애니메이션
        ani.SetTrigger("Training");
    }

    public void TrainingOnActive() // 애니메이션 훈련탭으로 전환
    {
        ani.SetTrigger("Training");
       
    }

    public void WeaponOnActive()//애니메이션 무기탭으로 전환
    {
        ani.SetTrigger("Weapon");
        
    }
   
    public void TeasureOnActive()//애니메이션 보물탭으로 전환
    {
        ani.SetTrigger("Teasure");
        
    }

    public void ShopOnActive()//애니메이션 상점탭으로 전환
    {
        ani.SetTrigger("Shop");
       
    }

    public void SearchOnActive()//애니메이션 수색탭으로 전환
    {
        ani.SetTrigger("Search");
        
    }
 
}
