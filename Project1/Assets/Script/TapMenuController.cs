using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapMenuController : MonoBehaviour
{
    //public GameObject Panel_weapon;
    //public GameObject Panel_Training;
    //public GameObject Panel_Shop;
    //public GameObject Panel_BattleField;
    //public GameObject Panel_Teasure;
    public Button[] Button_;
    public GameObject[] Tap_Panel;
    public int index;

    private void Start()
    {
        Tap_Panel[0].SetActive(true);
        for (int i = 1; i < index; i++)
        {
            Tap_Panel[i].SetActive(false);
        }
        //Panel_weapon.SetActive(false);
        //Panel_Training.SetActive(true);
        //Panel_Shop.SetActive(false);
        //Panel_BattleField.SetActive(false);
        //Panel_Teasure.SetActive(false);
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


}
