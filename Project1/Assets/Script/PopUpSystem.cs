using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PopUpSystem : MonoBehaviour
{
    public Text titleText;
    public Text contentsText;
    public GameObject PopUp;
    Action onClickOkay;
    Action onClickCancel;
    private static PopUpSystem instance;
    Animator ani;

    public static PopUpSystem GetInstance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<PopUpSystem>();
            if (instance == null)
            {
                GameObject container = new GameObject("DataController");
                instance = container.AddComponent<PopUpSystem>();
            }
        }
        return instance;
    }
    private void Awake()
    {
        ani = PopUp.GetComponent<Animator>();
    }
    public void OpenPopUp(string Title , string Contents, Action onClickOkay, Action onClickCancel)
    {
        titleText.text = Title;
        contentsText.text = Contents;
        this.onClickOkay = onClickOkay;
        this.onClickCancel = onClickCancel;
        PopUp.SetActive(true);
    }
    public void ClosePopUp()
    {
        ani.SetTrigger("Close");
        PopUp.SetActive(false);
    }
    public void OnClickOkay() 
    {
        if (onClickOkay != null)
        {
            OnClickOkay();
        }
        ClosePopUp();
        
    }
    public void OnClickCancel() 
    {
        if (onClickOkay != null)
        {
            OnClickCancel();
        }
        ClosePopUp();

    }
}
