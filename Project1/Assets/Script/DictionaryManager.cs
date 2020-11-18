using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DictionaryManager : MonoBehaviour
{
    public static DictionaryManager instance;

    public static DictionaryManager GetInstance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<DictionaryManager>();
            if (instance == null)
            {
                GameObject container = new GameObject("DataController");
                instance = container.AddComponent<DictionaryManager>();
            }
        }
        return instance;
    }

    BossDictionary BD;

    public Button[] iconbutton;
    public GameObject SelectImage;
    private RectTransform rect;
    GameObject go;
    int Count;

    private void Start()
    {
        BD = GameObject.Find("Canvas").GetComponent<BossDictionary>();
        for (int i = 0; i < iconbutton.Length; i++)
        {
            rect = iconbutton[i].GetComponent<RectTransform>();
            iconbutton[i].image.sprite = BD.bossinfo[i].BossImage;
        }
    }
    public void ChangeSprite(string bossName)
    {
        for (int i = 0; i < BD.ChagneBossSprite.Count; i++)
        {
            if (BD.ChagneBossSprite[i].name == bossName)
            {
                iconbutton[i].image.sprite = BD.ChagneBossSprite[i];
            }
        }
    }
    void PrFabsproduce(int num)// 선택이미지 생성
    {

        if (Count == 0)
        {
            rect = iconbutton[num].GetComponent<RectTransform>();
            go = Instantiate(SelectImage, rect.position, Quaternion.identity);
            go.transform.parent = iconbutton[num].transform;
            go.transform.localScale = Vector3.one;
            Count++;
        }
        else if (Count > 0)
        {
            go.transform.parent = iconbutton[num].transform;
            go.transform.position = iconbutton[num].transform.position;
        }
    }
    public void ButtonON(int num)
    {
        SoundManager.instance.ButtonSound();
        PrFabsproduce(num);
    }
}
