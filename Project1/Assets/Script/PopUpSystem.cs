using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PopUpSystem : MonoBehaviour
{
    public Text contentsText;
    public GameObject PopUp;
    public GameObject go;
    public bool EnterDeongun;

    Action onClickOkay;
    Action onClickCancel;
    private static PopUpSystem instance;
    public Animator ani;

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
        instance = this;
        ani = PopUp.GetComponent<Animator>();
    }
    public void OpenPopUp(string Title , string Contents, Action onClickOkay, Action onClickCancel)
    {
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
        EnterDeongun = true;
        if (EnterDeongun)
        {
            DataController.GetInstance().SubTicket(1);
            Player.Instance.transform.position = new Vector3(-262.43f, Player.Instance.transform.position.y - 20f, Player.Instance.transform.position.z);
            ClosePopUp();
            MonsterSpawn.GetInstance().MonsterCount = 0;
            MonsterSpawn.GetInstance().transform.position = new Vector3(-254, MonsterSpawn.GetInstance().transform.position.y - 20, MonsterSpawn.GetInstance().transform.position.z);
        }
    }
    public void OnClickCancel() 
    {
        ClosePopUp();
    }
}
