using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using Spine;

public class Fadeinout : MonoBehaviour
{

    public GameObject SearchRewardPanel;
    public GameObject SkeletonGraphic;
    public Image Panel;
    SkeletonGraphic skeletonAni;
    float time = 0f;
    float f_time = 1f;
    int TouchCount;
    bool aniCheck;
    public bool Win;
    public bool Lose;
    public Animator ani;
    public bool ClickOn ;
    public void Start()
    {
        skeletonAni = SearchRewardPanel.gameObject.GetComponent<SkeletonGraphic>();
        ClickOn = true;
    }
    public void Fade()
    {
        StartCoroutine(FadeFlow());
    }
    IEnumerator FadeFlow()
    {
        Panel.gameObject.SetActive(true);
        time = 0f;
        Color AlPha = Panel.color;
        while (AlPha.a < 1f)
        {
            time += Time.deltaTime * 5f;
            AlPha.a = Mathf.Lerp(0,1, time);
            Panel.color = AlPha;
            yield return null;
        }
        time = 0f;
        yield return new WaitForSeconds(0.8f);
        while (AlPha.a >0f)
        {
            time += Time.deltaTime * 5f;
            AlPha.a = Mathf.Lerp(1, 0, time);
            Panel.color = AlPha;
            yield return null;
        }
        Panel.gameObject.SetActive(false);
        yield return null;
    }
    public void SearchReward()
    {
        SkeletonGraphic.SetActive(true);
    }
    private void Update()
    {
        if (SkeletonGraphic.activeSelf == true)
        {
            //aniCheck = true;
            
            if (Win)
            {
                Lose = false;
                if (ClickOn == true) 
                {
                    
                    if (/*aniCheck == true && */TouchCount == 0)
                    {
                        ani.speed = 0;
                        //aniCheck = false;
                        
                        skeletonAni.AnimationState.AddAnimation(0, "2", true, 0);
                    }
                    if (Input.GetMouseButtonDown(0))
                    {
                        TouchCount++;
                        if (TouchCount == 1)
                        {
                            getitem();
                            StartCoroutine("rewardCoroutine");
                            skeletonAni.SkeletonDataAsset.GetAnimationStateData().SetMix("2", "3", 0.4f);
                            skeletonAni.AnimationState.SetAnimation(0, "3", false);
                            skeletonAni.AnimationState.AddAnimation(0, "4", false, 0);
                            skeletonAni.AnimationState.AddAnimation(0, "5-1", false, 0);
                            skeletonAni.AnimationState.AddAnimation(0, "5-2", true, 0);
                            
                        }
                        else if (TouchCount >= 2)
                        {
                            //skeletonAni.AnimationState.SetEmptyAnimations(0);
                            skeletonAni.AnimationState.SetEmptyAnimations(0);
                            skeletonAni.AnimationState.SetAnimation(0, "1", false);
                            ani.SetBool("Treasure1", false);
                            ani.SetBool("Treasure2", false);
                            SkeletonGraphic.SetActive(false);
                            TouchCount = 0;
                            Win = false;
                        }

                    }

                }
                
            }
            else if (Lose)
            {
                
                Win = false;
                if (/*aniCheck == true && */TouchCount == 0)
                {
                    //aniCheck = false;
                    skeletonAni.AnimationState.AddAnimation(0, "2", true, 0);
                }
                if (Input.GetMouseButtonDown(0))
                {
                    TouchCount++;
                   
                    if (TouchCount == 1)
                    {
                        skeletonAni.SkeletonDataAsset.GetAnimationStateData().SetMix("2", "6-1", 0.4f);
                        skeletonAni.AnimationState.SetAnimation(0, "6-1", false);
                        skeletonAni.AnimationState.AddAnimation(0, "6-2", false,0);
                        SoundManager.instance.Treasurefail();

                    }
                    else if (TouchCount >= 2)
                    {
                        skeletonAni.AnimationState.SetEmptyAnimations(0);
                        skeletonAni.AnimationState.SetAnimation(0, "1", false);
                        
                        //skeletonAni.AnimationState.SetAnimation(0, "1", false);
                        SkeletonGraphic.SetActive(false);
                        TouchCount = 0;
                        Lose = false;
                    }

                }
            }
        }
    }
    void getitem() 
    {
        if (SearchButton.Getitme == true)
        {
            ani.SetBool("Treasure1", true);
        }
        else
        {
            ani.SetBool("Treasure2", true);
        }
    }
    IEnumerator rewardCoroutine() 
    {
        ClickOn = false;
        yield return new WaitForSeconds(2.6f);
        ani.speed = 1;
        SoundManager.instance.TreasureSuccess();
        ClickOn = true;
    }
    
}
