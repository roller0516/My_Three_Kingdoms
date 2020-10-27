using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class BossInfo
{
    public string BossName;
    public bool EquipBoss;
    public Sprite BossImage;

    public BossInfo(string bossName, bool equipBoss, Sprite bossImage)
    {
        BossName = bossName;
        EquipBoss = equipBoss;
        BossImage = bossImage;
    }
}
public class BossDictionary : MonoBehaviour
{
    public static BossDictionary instance;

    public static BossDictionary GetInstance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<BossDictionary>();
            if (instance == null)
            {
                GameObject container = new GameObject("BattleFieldPanel");
                instance = container.AddComponent<BossDictionary>();
            }
        }
        return instance;
    }
    public List<BossInfo> bossinfo = new List<BossInfo>();
    public List<Sprite> ChagneBossSprite = new List<Sprite>();
    public Button[] iconbutton;
    public Button[] Equipbutton;
    public GameObject SelectImage;
    public GameObject CeatureButton;
    public Image Bossim;
    private RectTransform rect;
    public int EquipCount;
    GameObject go;
    int Count;
    public int num;
    public string BossName;
    public int prevnum;
    private void Start()
    {
        ListAddInfo();
        rect = GetComponent<RectTransform>();
        for (int i = 0; i < iconbutton.Length; i++)
        {
            iconbutton[i].image.sprite = bossinfo[i].BossImage;
        }
    }
    void Add(string bossName, bool equipBoss)
    {
        bossinfo.Add(new BossInfo(bossName, equipBoss, Resources.Load<Sprite>("UI/BossDictionary/" + bossName)));
    }
    public void AddChangeSprite(string SpriteName)
    {
        ChagneBossSprite.Add(Resources.Load<Sprite>("UI/BossDictionary/" + SpriteName));
    }
    public void ChangeSprite(string bossName)
    {
        for (int i = 0; i < ChagneBossSprite.Count; i++)
        {
            if (ChagneBossSprite[i].name == bossName)
            {
                iconbutton[i].image.sprite = ChagneBossSprite[i];
                BossName = bossName;
            }
        }
    }
    public void ButtonON(int num) //버튼을 눌렀어요
    {
        SoundManager.instance.ButtonSound();
        PrFabsproduce(num);
    }
    
    void ListAddInfo()
    {
        Add("NoneStage1boss", false);
        Add("NoneStage2boss", false);
        Add("NoneStage3boss", false);
        Add("NoneStage4boss", false);
        Add("NoneStage5boss", false);
        Add("NoneStage6boss", false);
        Add("NoneStage7boss", false);
        Add("NoneStage8boss", false);


        AddChangeSprite("Stage1boss");
        AddChangeSprite("Stage2boss");
        AddChangeSprite("Stage3boss");
        AddChangeSprite("Stage4boss");
        AddChangeSprite("Stage5boss");
        AddChangeSprite("Stage6boss");
        AddChangeSprite("Stage7boss");
        AddChangeSprite("Stage8boss");
    }

    void PrFabsproduce(int num)// 선택이미지 생성
    {
        this.num = num;
        
        if (Count == 0)
        {
            rect = iconbutton[num].GetComponent<RectTransform>();
            go = Instantiate(SelectImage, rect.position, Quaternion.identity);
            go.transform.parent = iconbutton[num].transform;
            go.transform.localScale = Vector3.one;
            Bossim.sprite = Resources.Load<Sprite>("UI/BossDictionary/Stage" + (num+1).ToString() + "boss");
            CeatureButton.GetComponent<SKillCooltime>().CreatureName = BossName;
            Equipbutton[num].gameObject.SetActive(true);
            prevnum = num;
            Count++;
        }
        else if (Count > 0)
        {
            if (prevnum != num) 
            {
                Equipbutton[num].gameObject.SetActive(true);
                for (int i = 0; i < Equipbutton.Length; i++) 
                {
                    if (i == num) 
                    {
                        continue;
                    }
                    Equipbutton[i].gameObject.SetActive(false); 
                }
            }
            Bossim.sprite = Resources.Load<Sprite>("UI/BossDictionary/Stage" + (num + 1).ToString() + "boss");
            CeatureButton.GetComponent<SKillCooltime>().CreatureName = BossName;
            go.transform.parent = iconbutton[num].transform;
            go.transform.position = iconbutton[num].transform.position;
        }
    }

    
}
