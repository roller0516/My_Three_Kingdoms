using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using ProjectD;

public class PopUpSystem : MonoBehaviour
{
    public TextMeshProUGUI contentsText;
    public GameObject PopUp;
    public bool EnterDeongun;
    public GameObject buttonobj;
    public GameObject Contentsobj;
    public GameObject ad;
    public GameObject ad2;
    Action onClickOkay;
    Action onClickCancel;
    private static PopUpSystem instance;
    public Animator ani;
    public bool ADView;

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
        buttonobj.SetActive(true);
        Contentsobj.SetActive(true);
    }

    public void ClosePopUp()
    {

        ani.SetTrigger("Close");
        PopUp.SetActive(false);
    }
    public void OnClickOkay() 
    {
        if (DataController.GetInstance().GetTicket() >= 1&& MonsterSpawn.GetInstance().Teleport)
        {
            
            EnterDeongun = true;
            MonsterSpawn.GetInstance().BossSummonON = false;
            if (EnterDeongun)
            {
                MonsterSpawn.GetInstance().fade.Fade();
                SoundManager.instance.BgmSound(3);
                DataController.GetInstance().SubTicket(1);
                StartCoroutine("teleport");
                MonsterSpawn.GetInstance().MonsterCount = 0;
                MonsterSpawn.GetInstance().transform.position = new Vector3(-254, MonsterSpawn.GetInstance().transform.position.y - 20, MonsterSpawn.GetInstance().transform.position.z);
                contentsText.text = "수색중입니다...";
                buttonobj.SetActive(false);
                Contentsobj.SetActive(false);
            }
        }
        else if(DataController.GetInstance().GetTicket() ==0)
        {
            ad2.gameObject.SetActive(true);
        }
    }
    public void AdEnterDeongun() 
    {
        if (DataController.GetInstance().GetTicket() ==0)
        {
            ad.SetActive(false);
            ad2.gameObject.SetActive(true);
            return;
        }
        else if(MonsterSpawn.GetInstance().Teleport)
            AdService.Instance.ShowInterstitial(FreeEnterDeongun);
        ADView = true;
    }
    void FreeEnterDeongun() 
    {
        if (DataController.GetInstance().GetTicket() >= 1 )
        {
            EnterDeongun = true;
            MonsterSpawn.GetInstance().BossSummonON = false;
            if (EnterDeongun)
            {
                MonsterSpawn.GetInstance().fade.Fade();
                SoundManager.instance.BgmSound(3);
                DataController.GetInstance().SubTicket(1);
                StartCoroutine("teleport");
                MonsterSpawn.GetInstance().MonsterCount = 0;
                MonsterSpawn.GetInstance().transform.position = new Vector3(-254, MonsterSpawn.GetInstance().transform.position.y - 20, MonsterSpawn.GetInstance().transform.position.z);
                contentsText.text = "수색중입니다...";
                buttonobj.SetActive(false);
                Contentsobj.SetActive(false);
            }
        }
    }
    IEnumerator teleport() 
    {
        yield return new WaitForSeconds(0.85f);
        MonsterSpawn.GetInstance().fade.Fade();
        yield return new WaitForSeconds(0.2f);
        Player.Instance.transform.position = new Vector3(-262.43f, Player.Instance.transform.position.y - 20f, Player.Instance.transform.position.z);
    }
    public void AddTicketAD() 
    {
        AdService.Instance.ShowInterstitial(AddTicket);
    }
    void AddTicket() 
    {
        DataController.GetInstance().AddTicket(3);
    }
    public void OpenADpopUP() 
    {
        ad.SetActive(true);
    }

    public void OnADPoPUpYes() 
    {
        AdEnterDeongun();
    }
    public void OnADPoPUpNo()
    {
        ad.SetActive(false);
    }
    public void OnADPoPUpNo2()
    {
        
        ad2.SetActive(false);
    }
    public void OnClickCancel() 
    {
        ClosePopUp();
    }
}
