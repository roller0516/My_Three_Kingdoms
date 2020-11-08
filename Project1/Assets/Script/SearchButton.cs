using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SearchButton : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public string S_Name;
    public int RandomRange1;
    public int ItemRandomRange;
    public int probability;
    SpecialitemList sl;
    public bool isWin;
    bool timerTrue;
    public Image[] itemImage;
    public Image rewardimage;
    public static bool Getitme;
    int RewardGold;
    int RewardKnowledge;
    private void Start()
    {
        sl = GameObject.Find("Canvas").GetComponent<SpecialitemList>();
    }
    public void PlayerTeleport()
    {
        UIManager.GetInstance().SearchName = GetName();
        if (PopUpSystem.GetInstance().PopUp.activeSelf == true)
            PopUpSystem.GetInstance().ClosePopUp();
        else
        {
            PopUpSystem.GetInstance().OpenPopUp("알림", this.S_Name + "으로 수색 하겠습니까?", () => { Debug.Log("Open"); }, () => { Debug.Log("Close"); });
            SetitemImage(S_Name);
        }
    }
    void SetitemImage(string imgName)
    {
        print(imgName);
        switch (imgName)
        {
            case "하북":
                itemImage[0].sprite = Resources.Load<Sprite>("UI/Treasure/special1");
                itemImage[1].sprite = Resources.Load<Sprite>("UI/Treasure/special2");
                break;
            case "청서":
                itemImage[0].sprite = Resources.Load<Sprite>("UI/Treasure/special3");
                itemImage[1].sprite = Resources.Load<Sprite>("UI/Treasure/special4");
                break;
            case "중원":
                itemImage[0].sprite = Resources.Load<Sprite>("UI/Treasure/special5");
                itemImage[1].sprite = Resources.Load<Sprite>("UI/Treasure/special6");
                break;
            case "강동":
                itemImage[0].sprite = Resources.Load<Sprite>("UI/Treasure/special7");
                itemImage[1].sprite = Resources.Load<Sprite>("UI/Treasure/special8");
                break;
            case "관중":
                itemImage[0].sprite = Resources.Load<Sprite>("UI/Treasure/special9");
                itemImage[1].sprite = Resources.Load<Sprite>("UI/Treasure/special10");
                break;
            case "형북":
                itemImage[0].sprite = Resources.Load<Sprite>("UI/Treasure/special11");
                itemImage[1].sprite = Resources.Load<Sprite>("UI/Treasure/special12");
                break;
            case "형남":
                itemImage[0].sprite = Resources.Load<Sprite>("UI/Treasure/special13");
                itemImage[1].sprite = Resources.Load<Sprite>("UI/Treasure/special14");
                break;
            case "파촉":
                itemImage[0].sprite = Resources.Load<Sprite>("UI/Treasure/special15");
                itemImage[1].sprite = Resources.Load<Sprite>("UI/Treasure/special16");
                break;
        }

    }
    public void Win(bool WIN)
    {
        if (WIN == true)
        {
            PopUpSystem.GetInstance().ClosePopUp();
            FindObjectOfType<Fadeinout>().GetComponent<Fadeinout>().Win = true;
            MonsterSpawn.GetInstance().stg.stageSound();
            switch (S_Name)
            {
                case "하북":
                    print(S_Name);
                    RewardGold = Random.Range(2, 11);
                    RewardKnowledge = Random.Range(50, 116);
                    RewardGold = sl.Upgrade * RewardGold;
                    DataController.GetInstance().AddPaidGold(RewardGold);
                    DataController.GetInstance().AddKnowledge(RewardKnowledge);
                    if (RandomRange1 <= 10)
                    {
                        print("아이템 얻음");
                        Getitme = true;
                        if (sl.Sp_item[0].itemCount < 10 && ItemRandomRange <= 50)
                        {
                            rewardimage.sprite = Resources.Load<Sprite>("UI/Treasure/special1");
                            sl.Sp_item[0].itemCount++;
                        }
                        else if (sl.Sp_item[1].itemCount < 10 && ItemRandomRange >= 51)
                        {
                            rewardimage.sprite = Resources.Load<Sprite>("UI/Treasure/special2");
                            sl.Sp_item[1].itemCount++;
                        }
                        else if (sl.Sp_item[0].itemCount == 10 && sl.Sp_item[1].itemCount < 10)
                        {
                            rewardimage.sprite = Resources.Load<Sprite>("UI/Treasure/special2");
                            sl.Sp_item[1].itemCount++;
                        }
                        else if (sl.Sp_item[1].itemCount == 10&& sl.Sp_item[0].itemCount < 10) 
                        {
                            rewardimage.sprite = Resources.Load<Sprite>("UI/Treasure/special1");
                            sl.Sp_item[0].itemCount++;
                        }
                        else 
                        {
                            Getitme = false;
                        }
                        
                    }
                    else
                    {
                        Getitme = false;
                    }
                    break;
                case "청서":
                    print(S_Name);
                    RewardGold = Random.Range(2, 11);
                    RewardKnowledge = Random.Range(50, 116);
                    RewardGold = sl.Upgrade * RewardGold;
                    DataController.GetInstance().AddPaidGold(RewardGold);
                    DataController.GetInstance().AddKnowledge(RewardKnowledge);
                    if (RandomRange1 <= 10)
                    {
                        print("아이템 얻음");
                        
                        Getitme = true;
                        if (sl.Sp_item[2].itemCount < 10 && ItemRandomRange <= 50)
                        {
                            rewardimage.sprite = Resources.Load<Sprite>("UI/Treasure/special3");
                            sl.Sp_item[2].itemCount++;
                        }
                        else if (sl.Sp_item[3].itemCount < 10 && ItemRandomRange >= 51)
                        {
                            rewardimage.sprite = Resources.Load<Sprite>("UI/Treasure/special4");
                            sl.Sp_item[3].itemCount++;
                        }
                        else if (sl.Sp_item[2].itemCount == 10&& sl.Sp_item[3].itemCount<10)
                        {
                            rewardimage.sprite = Resources.Load<Sprite>("UI/Treasure/special4");
                            sl.Sp_item[3].itemCount++;
                        }
                        else if (sl.Sp_item[3].itemCount == 10&& sl.Sp_item[2].itemCount<10)
                        {
                            rewardimage.sprite = Resources.Load<Sprite>("UI/Treasure/special3");
                            sl.Sp_item[2].itemCount++;
                        }
                        else 
                        {
                            Getitme = false;
                        }
                    }
                    else
                    {
                        Getitme = false;
                    }
                    break;

                case "중원":
                    print(S_Name);
                    RewardGold = Random.Range(2, 11);
                    RewardKnowledge = Random.Range(50, 116);
                    RewardGold = sl.Upgrade * RewardGold;
                    DataController.GetInstance().AddPaidGold(RewardGold);
                    DataController.GetInstance().AddKnowledge(RewardKnowledge);
                    if (RandomRange1 <= 10)
                    {
                        print("아이템 얻음");
                        
                        Getitme = true;
                        if (sl.Sp_item[4].itemCount < 10 && ItemRandomRange <= 50)
                        {
                            rewardimage.sprite = Resources.Load<Sprite>("UI/Treasure/special5");
                            sl.Sp_item[4].itemCount++;
                        }
                        else if (sl.Sp_item[5].itemCount < 10 && ItemRandomRange >= 51)
                        {
                            rewardimage.sprite = Resources.Load<Sprite>("UI/Treasure/special6");
                            sl.Sp_item[5].itemCount++;
                        }
                        else if (sl.Sp_item[4].itemCount == 10&& sl.Sp_item[5].itemCount<10)
                        {
                            rewardimage.sprite = Resources.Load<Sprite>("UI/Treasure/special6");
                            sl.Sp_item[5].itemCount++;
                        }
                        else if (sl.Sp_item[5].itemCount == 10&& sl.Sp_item[4].itemCount<10)
                        {
                            rewardimage.sprite = Resources.Load<Sprite>("UI/Treasure/special5");
                            sl.Sp_item[4].itemCount++;
                        }
                        else 
                        {
                            Getitme = false;
                        }
                    }
                    else
                    {
                        Getitme = false;
                    }
                    break;

                case "강동":
                    print(S_Name);
                    RewardGold = Random.Range(2, 11);
                    RewardKnowledge = Random.Range(50, 116);
                    RewardGold = sl.Upgrade * RewardGold;
                    DataController.GetInstance().AddPaidGold(RewardGold);
                    DataController.GetInstance().AddKnowledge(RewardKnowledge);
                    if (RandomRange1 <= 10)
                    {
                        
                        Getitme = true;
                        if (sl.Sp_item[6].itemCount < 10 && ItemRandomRange <= 50)
                        {
                            rewardimage.sprite = Resources.Load<Sprite>("UI/Treasure/special7");
                            sl.Sp_item[6].itemCount++;
                        }
                        else if (sl.Sp_item[7].itemCount < 10 && ItemRandomRange >= 51)
                        {
                            rewardimage.sprite = Resources.Load<Sprite>("UI/Treasure/special8");
                            sl.Sp_item[7].itemCount++;
                        }
                        else if (sl.Sp_item[6].itemCount == 10&& sl.Sp_item[5].itemCount<10)
                        {
                            rewardimage.sprite = Resources.Load<Sprite>("UI/Treasure/special8");
                            sl.Sp_item[7].itemCount++;
                        }
                        else if (sl.Sp_item[7].itemCount == 10&& sl.Sp_item[6].itemCount<10)
                        {
                            rewardimage.sprite = Resources.Load<Sprite>("UI/Treasure/special7");
                            sl.Sp_item[6].itemCount++;
                        }
                        else 
                        {
                            Getitme = false;
                        }
                    }
                    else
                    {
                        Getitme = false;
                    }

                    break;
                case "관중":
                    print(S_Name);
                    if (RandomRange1 <= 10)
                    {
                        RewardGold = Random.Range(2, 11);
                        RewardKnowledge = Random.Range(50, 116);
                        RewardGold = sl.Upgrade * RewardGold;
                        DataController.GetInstance().AddPaidGold(RewardGold);
                        DataController.GetInstance().AddKnowledge(RewardKnowledge);
                        Getitme = true;
                        if (sl.Sp_item[8].itemCount < 10 && ItemRandomRange <= 50)
                        {
                            rewardimage.sprite = Resources.Load<Sprite>("UI/Treasure/special9");
                            sl.Sp_item[8].itemCount++;
                        }
                        else if (sl.Sp_item[9].itemCount < 10 && ItemRandomRange >= 51)
                        {
                            rewardimage.sprite = Resources.Load<Sprite>("UI/Treasure/special10");
                            sl.Sp_item[9].itemCount++;
                        }
                        else if (sl.Sp_item[8].itemCount ==10 && sl.Sp_item[9].itemCount<10)
                        {
                            rewardimage.sprite = Resources.Load<Sprite>("UI/Treasure/special10");
                            sl.Sp_item[9].itemCount++;
                        }
                        else if (sl.Sp_item[9].itemCount ==10 && sl.Sp_item[8].itemCount<10)
                        {
                            rewardimage.sprite = Resources.Load<Sprite>("UI/Treasure/special9");
                            sl.Sp_item[8].itemCount++;
                        }
                        else 
                        {
                            Getitme = false;
                        }
                    }
                    else
                    {
                        Getitme = false;
                    }

                    break;
                case "형북":
                    print(S_Name);
                    if (RandomRange1 <= 10)
                    {
                        RewardGold = Random.Range(2, 11);
                        RewardKnowledge = Random.Range(50, 116);
                        RewardGold = sl.Upgrade * RewardGold;
                        DataController.GetInstance().AddPaidGold(RewardGold);
                        DataController.GetInstance().AddKnowledge(RewardKnowledge);
                        Getitme = true;
                        if (sl.Sp_item[10].itemCount < 10 && ItemRandomRange <= 50)
                        {
                            rewardimage.sprite = Resources.Load<Sprite>("UI/Treasure/special11");
                            sl.Sp_item[10].itemCount++;
                        }
                        else if (sl.Sp_item[11].itemCount < 10 && ItemRandomRange >= 51)
                        {
                            rewardimage.sprite = Resources.Load<Sprite>("UI/Treasure/special12");
                            sl.Sp_item[11].itemCount++;
                        }
                        else if (sl.Sp_item[10].itemCount == 10 && sl.Sp_item[11].itemCount<10)
                        {
                            rewardimage.sprite = Resources.Load<Sprite>("UI/Treasure/special12");
                            sl.Sp_item[11].itemCount++;
                        }
                        else if (sl.Sp_item[11].itemCount == 10 && sl.Sp_item[10].itemCount<10)
                        {
                            rewardimage.sprite = Resources.Load<Sprite>("UI/Treasure/special11");
                            sl.Sp_item[10].itemCount++;
                        }
                        else 
                        {
                            Getitme = false;
                        }
                    }
                    else
                    {
                        Getitme = false;
                    }

                    break;
                case "형남":
                    print(S_Name);
                    if (RandomRange1 <= 10)
                    {
                        RewardGold = Random.Range(2, 11);
                        RewardKnowledge = Random.Range(50, 116);
                        RewardGold = sl.Upgrade * RewardGold;
                        DataController.GetInstance().AddPaidGold(RewardGold);
                        DataController.GetInstance().AddKnowledge(RewardKnowledge);
                        Getitme = true;
                        if (sl.Sp_item[12].itemCount < 10 && ItemRandomRange <= 50)
                        {
                            rewardimage.sprite = Resources.Load<Sprite>("UI/Treasure/special13");
                            sl.Sp_item[12].itemCount++;
                        }
                        else if (sl.Sp_item[13].itemCount < 10 && ItemRandomRange >= 51)
                        {
                            rewardimage.sprite = Resources.Load<Sprite>("UI/Treasure/special14");
                            sl.Sp_item[13].itemCount++;
                        }
                        else if (sl.Sp_item[12].itemCount == 10 && sl.Sp_item[13].itemCount<10)
                        {
                            rewardimage.sprite = Resources.Load<Sprite>("UI/Treasure/special14");
                            sl.Sp_item[13].itemCount++;
                        }
                        else if (sl.Sp_item[13].itemCount == 10 && sl.Sp_item[12].itemCount<10)
                        {
                            rewardimage.sprite = Resources.Load<Sprite>("UI/Treasure/special13");
                            sl.Sp_item[12].itemCount++;
                        }
                        else 
                        {
                            Getitme = false;
                        }
                    }
                    else
                    {
                        Getitme = false;
                    }

                    break;
                case "파촉":
                    print(S_Name);
                    if (RandomRange1 <= 10)
                    {
                        RewardGold = Random.Range(20, 30);
                        RewardKnowledge = Random.Range(50, 116);
                        RewardGold = sl.Upgrade * RewardGold;
                        DataController.GetInstance().AddPaidGold(RewardGold);
                        DataController.GetInstance().AddKnowledge(RewardKnowledge);
                        Getitme = true;
                        if (sl.Sp_item[14].itemCount < 10 && ItemRandomRange <= 50)
                        {
                            rewardimage.sprite = Resources.Load<Sprite>("UI/Treasure/special15");
                            sl.Sp_item[14].itemCount++;
                        }
                        else if (sl.Sp_item[15].itemCount < 10 && ItemRandomRange >= 51)
                        {
                            rewardimage.sprite = Resources.Load<Sprite>("UI/Treasure/special6");
                            sl.Sp_item[15].itemCount++;
                        }
                        else if (sl.Sp_item[14].itemCount == 10 && sl.Sp_item[15].itemCount < 10)
                        {
                            rewardimage.sprite = Resources.Load<Sprite>("UI/Treasure/special6");
                            sl.Sp_item[15].itemCount++;
                        }
                        else if (sl.Sp_item[15].itemCount == 10 && sl.Sp_item[14].itemCount <10)
                        {
                            rewardimage.sprite = Resources.Load<Sprite>("UI/Treasure/special15");
                            sl.Sp_item[14].itemCount++;
                        }
                        else
                        {
                            Getitme = false;
                        }
                    }
                    else
                    {
                        Getitme = false;
                    }
                    break;
            }
            DataController.GetInstance().SaveSpecialitem(sl);
        }
    }
    private void Update()
    {
        RandomRange1 = Random.Range(1,101);
        ItemRandomRange = Random.Range(1, 101);
        
    }
    public string GetName()
    {
        return S_Name;
    }
    public void SetName(string name)
    {
        S_Name = name;
    }
    
}
