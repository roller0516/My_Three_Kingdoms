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
            FindObjectOfType<Fadeinout>().GetComponent<Fadeinout>().Win = true;
            switch (S_Name)
            {

                case "하북":
                    if (RandomRange1 <= 30)
                    {
                        print("1");

                        FindObjectOfType<Fadeinout>().GetComponent<Fadeinout>().ani.SetTrigger("Treasure1");
                        if (sl.Sp_item[0].itemCount < 10 && ItemRandomRange <= 50)
                        {
                            rewardimage.sprite =  Resources.Load<Sprite>("UI/Treasure/special1");

                            sl.Sp_item[0].itemCount++;
                            print(sl.Sp_item[0].itemCount);
                        }
                        else if (sl.Sp_item[1].itemCount < 10  && ItemRandomRange >= 51)
                        {
                            rewardimage.sprite = Resources.Load<Sprite>("UI/Treasure/special2");
                            sl.Sp_item[1].itemCount++;
                            print(sl.Sp_item[1].itemCount);
                        }
                    }
                    break;
                case "청서":
                    if (RandomRange1 <= 30)
                    {
                        FindObjectOfType<Fadeinout>().GetComponent<Fadeinout>().ani.SetTrigger("Treasure1");
                        if (sl.Sp_item[2].itemCount < 10 && ItemRandomRange <= 50)
                        {
                            rewardimage.sprite = Resources.Load<Sprite>("UI/Treasure/special3");
                            sl.Sp_item[2].itemCount++;
                            print(sl.Sp_item[2].itemCount);
                        }
                        else if (sl.Sp_item[3].itemCount < 10 && ItemRandomRange >= 51)
                        {
                            rewardimage.sprite = Resources.Load<Sprite>("UI/Treasure/special4");
                            sl.Sp_item[3].itemCount++;
                            print(sl.Sp_item[3].itemCount);
                        }
                    }
                    break;

                case "중원":

                    if (RandomRange1 <= 30)
                    {
                        FindObjectOfType<Fadeinout>().GetComponent<Fadeinout>().ani.SetTrigger("Treasure1");
                        if (sl.Sp_item[4].itemCount < 10 && ItemRandomRange <= 50)
                        {
                            rewardimage.sprite = Resources.Load<Sprite>("UI/Treasure/special5");
                            sl.Sp_item[4].itemCount++;
                            print(sl.Sp_item[4].itemCount);
                        }
                        else if (sl.Sp_item[5].itemCount < 10 && ItemRandomRange >= 51)
                        {
                            rewardimage.sprite = Resources.Load<Sprite>("UI/Treasure/special6");
                            sl.Sp_item[5].itemCount++;
                            print(sl.Sp_item[5].itemCount);
                        }
                    }


                    break;

                case "강동":
                    if (RandomRange1 <= 30)
                    {
                        FindObjectOfType<Fadeinout>().GetComponent<Fadeinout>().ani.SetTrigger("Treasure1");
                        if (sl.Sp_item[6].itemCount < 10 && ItemRandomRange <= 50)
                        {
                            rewardimage.sprite = Resources.Load<Sprite>("UI/Treasure/special7");
                            sl.Sp_item[6].itemCount++;
                            print(sl.Sp_item[6].itemCount);
                        }
                        else if (sl.Sp_item[7].itemCount < 10 && ItemRandomRange >= 51)
                        {
                            rewardimage.sprite = Resources.Load<Sprite>("UI/Treasure/special8");
                            sl.Sp_item[7].itemCount++;
                            print(sl.Sp_item[7].itemCount);
                        }
                    }
                    break;

                case "관중":
                    if (RandomRange1 <= 30)
                    {
                        FindObjectOfType<Fadeinout>().GetComponent<Fadeinout>().ani.SetTrigger("Treasure1");
                        if (sl.Sp_item[8].itemCount < 10 && ItemRandomRange <= 50)
                        {
                            rewardimage.sprite = Resources.Load<Sprite>("UI/Treasure/special9");
                            sl.Sp_item[8].itemCount++;
                            print(sl.Sp_item[8].itemCount);
                        }
                        else if (sl.Sp_item[9].itemCount < 10 && ItemRandomRange >= 51)
                        {
                            rewardimage.sprite = Resources.Load<Sprite>("UI/Treasure/special10");
                            sl.Sp_item[9].itemCount++;
                            print(sl.Sp_item[9].itemCount);
                        }
                    }
                    break;
                case "형북":
                    if (RandomRange1 <= 30)
                    {
                        FindObjectOfType<Fadeinout>().GetComponent<Fadeinout>().ani.SetTrigger("Treasure1");
                        if (sl.Sp_item[10].itemCount < 10 && ItemRandomRange <= 50)
                        {
                            rewardimage.sprite = Resources.Load<Sprite>("UI/Treasure/special11");
                            sl.Sp_item[10].itemCount++;
                            print(sl.Sp_item[10].itemCount);
                        }
                        else if (sl.Sp_item[11].itemCount < 10 && ItemRandomRange >= 51)
                        {
                            rewardimage.sprite = Resources.Load<Sprite>("UI/Treasure/special12");
                            sl.Sp_item[11].itemCount++;
                            print(sl.Sp_item[11].itemCount);
                        }
                    }
                    break;
                case "형남":
                    if (RandomRange1 <= 30)
                    {
                        FindObjectOfType<Fadeinout>().GetComponent<Fadeinout>().ani.SetTrigger("Treasure1");
                        if (sl.Sp_item[12].itemCount < 10 && ItemRandomRange <= 50)
                        {
                            rewardimage.sprite = Resources.Load<Sprite>("UI/Treasure/special13");
                            sl.Sp_item[12].itemCount++;
                            print(sl.Sp_item[12].itemCount);
                        }
                        else if (sl.Sp_item[13].itemCount < 10 && ItemRandomRange >= 51)
                        {
                            rewardimage.sprite = Resources.Load<Sprite>("UI/Treasure/special14");
                            sl.Sp_item[13].itemCount++;
                            print(sl.Sp_item[13].itemCount);
                        }
                    }
                    break;
                case "파촉":
                    if (RandomRange1 <= 30)
                    {
                        FindObjectOfType<Fadeinout>().GetComponent<Fadeinout>().ani.SetTrigger("Treasure1");
                        if (sl.Sp_item[14].itemCount < 10 && ItemRandomRange <= 50)
                        {
                            rewardimage.sprite = Resources.Load<Sprite>("UI/Treasure/special15");
                            sl.Sp_item[14].itemCount++;
                            print(sl.Sp_item[14].itemCount);
                        }
                        else if (sl.Sp_item[15].itemCount < 10 && ItemRandomRange >= 51)
                        {
                            rewardimage.sprite = Resources.Load<Sprite>("UI/Treasure/special6");
                            sl.Sp_item[15].itemCount++;
                            print(sl.Sp_item[15].itemCount);
                        }
                    }
                    break;
            }
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
