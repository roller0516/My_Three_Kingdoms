using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
[System.Serializable]
public class BossInfo
{
    public string BossName;
    public Sprite BossImage;
    public string Contents;
    public string BossNameText;
    public bool IsChange;
    public BossInfo(string bossName, Sprite bossImage, string contents,string bossNameText,bool isChange)
    {
        IsChange = isChange;
        BossNameText = bossNameText;
        Contents = contents;
        BossName = bossName;
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
    public TextMeshProUGUI BossNameTEXT;
    public TextMeshProUGUI ContentsTEXT;
    public GameObject SelectImage;
    public Image Bossim;
    public int num;
    public int prevnum;
    public string BossName;
    private RectTransform rect;
    private GameObject go;
    private int Count;
    
    private void Start()
    {
        ListAddInfo();
        rect = GetComponent<RectTransform>();
        for (int i = 0; i < iconbutton.Length; i++)
        {
            iconbutton[i].image.sprite = bossinfo[i].BossImage;
        }
        DataController.GetInstance().LoadBossDic(this);
    }
    private void Update()
    {
        changeSpriteCheck();
    }
    void Add(string bossName,string contents,string bossNameText)
    {
        bossinfo.Add(new BossInfo(bossName, Resources.Load<Sprite>("UI/BossDictionary/" + bossName), contents, bossNameText,false));
    }
    void AddChangeSprite(string SpriteName)
    {
        ChagneBossSprite.Add(Resources.Load<Sprite>("UI/BossDictionary/" + SpriteName));
    }
    public void ChangeSprite(string bossName)
    {
        for (int i = 0; i < ChagneBossSprite.Count; i++)
        {
            if (ChagneBossSprite[i].name == bossName)
            {
                bossinfo[i].IsChange = true;
                iconbutton[i].image.sprite = ChagneBossSprite[i];
                bossinfo[i].BossName = bossName;
            }
        }
        DataController.GetInstance().SaveBossDic(this);
    }
    public void ButtonON(int num) //버튼을 눌렀어요
    {
        SoundManager.instance.ButtonSound();
        PrFabsproduce(num);
        
    }
    void changeSpriteCheck()
    {
        for (int i = 0; i < bossinfo.Count; i++)
            ChangeSprite(bossinfo[i].BossName);
    }
    void ListAddInfo()
    {
        Add("NoneStage1boss","칼을 휘드른다","배원소");
        Add("NoneStage2boss", "칼을 휘드른다","정원지");
        Add("NoneStage3boss", "칼을 휘드른다","장보");
        Add("NoneStage4boss", "칼을 휘드른다","장량");
        Add("NoneStage5boss", "칼을 휘드른다","장각");
        Add("NoneStage6boss", "칼을 휘드른다","이각");
        Add("NoneStage7boss", "칼을 휘드른다","화웅");
        Add("NoneStage8boss", "칼을 휘드른다","왕윤");
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
            Bossim.gameObject.SetActive(true);
            Bossim.sprite = Resources.Load<Sprite>("UI/BossDictionary/Stage" + (num+1).ToString() + "boss_im");
            BossName = bossinfo[num].BossName;
            UIManager.GetInstance().equipButton[num].gameObject.SetActive(true);
            prevnum = num;
            Count++;
            Textcontents(num);
        }
        else if (Count > 0)
        {
            print(prevnum);
            print(num);
            if (prevnum != num)
            {
                UIManager.GetInstance().equipButton[num].gameObject.SetActive(true);
                for (int i = 0; i < UIManager.GetInstance().equipButton.Length; i++)
                {
                    if (i == num)
                    {
                        continue;
                    }
                    UIManager.GetInstance().equipButton[i].gameObject.SetActive(false);
                }
            }
            Textcontents(num);
            BossName = bossinfo[num].BossName;
            Bossim.gameObject.SetActive(true);
            Bossim.sprite = Resources.Load<Sprite>("UI/BossDictionary/Stage" + (num + 1).ToString() + "boss_im");
            go.transform.parent = iconbutton[num].transform;
            go.transform.position = iconbutton[num].transform.position;
            prevnum = num;
        }
    }
    void Textcontents(int num) 
    {
        BossNameTEXT.text = bossinfo[num].BossNameText;
        ContentsTEXT.text = bossinfo[num].Contents;
    }
}
