using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapMenuController : MonoBehaviour
{
    public Button[] Button_;
    public Button[] Button_event;
    public GameObject[] Tap_Panel;


    public int index;
    public float time;

    private void Start()
    {
        for (int i = 0; i < index; i++)
        {
            Tap_Panel[i].SetActive(false);
        }
        Tap_Panel[0].SetActive(true);
    }

    public void TrainingOnActive()
    {
        for (int i = 0; i < index; i++)
        {
            Tap_Panel[i].SetActive(false);
            if (Tap_Panel[0])
            {
                for (int j = 0; j < index; j++)
                {
                    Button_[j].interactable = true;
                    if (Button_[0])
                        Button_[0].interactable = false;
                }
                Tap_Panel[0].SetActive(true);
            }
                
        }
    }

    public void WeaponOnActive()
    {
        for (int i = 0; i < index; i++)
        {
            if (i ==0)
                continue;
            Tap_Panel[i].SetActive(false);
            if (Tap_Panel[1])
            {
                for (int j = 0; j < index; j++)
                {
                    Button_[j].interactable = true;
                    if (Button_[1])
                        Button_[1].interactable = false;
                }
                Tap_Panel[1].SetActive(true);
            }
        }
    }
   
    public void TeasureOnActive()
    {
        for (int i = 0; i < index; i++)
        {
            if (i == 0)
                continue;
            Tap_Panel[i].SetActive(false);
            if (Tap_Panel[2])
            {
                for (int j = 0; j < index; j++)
                {
                    Button_[j].interactable = true;
                    if (Button_[2])
                        Button_[2].interactable = false;
                }
                Tap_Panel[2].SetActive(true);
            }
        }
    }

    public void ShopOnActive()
    {
        for (int i = 0; i < index; i++)
        {
            if (i == 0)
                continue;
            Tap_Panel[i].SetActive(false);
            if (Tap_Panel[3])
            {
                for (int j = 0; j < index; j++)
                {
                    Button_[j].interactable = true;
                    if (Button_[3])
                        Button_[3].interactable = false;
                }
                Tap_Panel[3].SetActive(true);
            }
        }
    }

    public void BattleFieldOnActive()
    {
        for (int i = 0; i < index; i++)
        {
            if (i == 0)
                continue;
            Tap_Panel[i].SetActive(false);
            if (Tap_Panel[4])
            {
                for (int j = 0; j < index; j++)
                {
                    Button_[j].interactable = true;
                    if (Button_[4])
                        Button_[4].interactable = false;
                }
                Tap_Panel[4].SetActive(true);
            }
        }
    }
  
    void TeasureOpen()//조건의 맞는 상황이 되면 보물창 열림 시스템
    {
        if (time > 5 && Button_event[0] != null)
        {
            Button_event[0].gameObject.SetActive(false);
            time = 0;
            Destroy(Button_event[0].gameObject);
        }
    }

    void BattleFieldOpen()//조건의 맞는 상황이 되면 전장창 열림 시스템
    {
        if (time > 10 && Button_event[1] != null)
        {
            Button_event[1].gameObject.SetActive(false);
            time = 0;
            Destroy(Button_event[1].gameObject);
        }
    }

    private void Update()
    {
        TeasureOpen();
        BattleFieldOpen();
        time += Time.deltaTime;
    }

  
}
