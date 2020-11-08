using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;

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
            aniCheck = true;
            if (Win)
            {
                Lose = false;
                if (aniCheck == true && TouchCount == 0)
                {
                    ani.speed = 0;
                    aniCheck = false;
                    skeletonAni.AnimationState.AddAnimation(0, "2", true, 0);
                }
                if (ClickOn == true) 
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        TouchCount++;
                        if (TouchCount == 1)
                        {
                            getitem();
                            StartCoroutine("rewardCoroutine");
                            skeletonAni.AnimationState.SetAnimation(0, "3", false);
                            skeletonAni.AnimationState.AddAnimation(0, "4", false, 0);
                            skeletonAni.AnimationState.AddAnimation(0, "5-1", false, 0);
                            skeletonAni.AnimationState.AddAnimation(0, "5-2", true, 0);
                        }
                        else if (TouchCount >= 2)
                        {
                            ani.SetBool("Treasure1", false);
                            ani.SetBool("Treasure2", false);
                            skeletonAni.AnimationState.SetEmptyAnimations(0);
                            skeletonAni.AnimationState.SetAnimation(0, "1", false);

                            SkeletonGraphic.SetActive(false);
                            TouchCount = 0;
                        }

                    }
                }
            }
            else if (Lose)
            {
                FindObjectOfType<Fadeinout>().GetComponent<Fadeinout>().ani.SetTrigger("Treasure2");
                ani.speed = 0;
                Win = false;
                if (Input.GetMouseButtonDown(0))
                {
                    TouchCount++;
                    if (TouchCount == 1)
                    {
                        skeletonAni.AnimationState.SetAnimation(0, "6-1", true);
                        skeletonAni.AnimationState.AddAnimation(0, "6-2", false,0);
                        SoundManager.instance.Treasurefail();

                    }
                    else if (TouchCount >= 2)
                    {
                        skeletonAni.AnimationState.SetEmptyAnimations(0);
                        skeletonAni.AnimationState.SetAnimation(0, "1", false);

                        SkeletonGraphic.SetActive(false);
                        TouchCount = 0;
                    }

                }
            }
        }
    }
    void getitem() 
    {
        for (int i = 0; i < UIManager.GetInstance().searchButtons.Length; i++)
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
