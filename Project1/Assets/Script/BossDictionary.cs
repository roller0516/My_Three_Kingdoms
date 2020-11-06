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
    public Image[] iconbutton;
    public Image[] ribbon;
    public Image Bossim;
    
    public TextMeshProUGUI BossNameTEXT;
    public TextMeshProUGUI ContentsTEXT;
    public GameObject SelectImage;
    
    public string BossName;
    public int num;
    public int prevnum;

    private RectTransform rect;
    private GameObject go;
    private int Count;
    private void Start()
    {
        ListAddInfo();
        rect = GetComponent<RectTransform>();
        for (int i = 0; i < iconbutton.Length; i++)
        {
            iconbutton[i].sprite = bossinfo[i].BossImage;
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
                iconbutton[i].sprite = ChagneBossSprite[i];
                bossinfo[i].BossName = bossName;
                ribbon[i].sprite = Resources.Load<Sprite>("UI/BossDictionary/red");
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
        {
            ChangeSprite(bossinfo[i].BossName);
        }
           
    }
    void ListAddInfo()
    {
        Add("NoneStage1boss","무거운 양날 도끼를 휘두른다","배원소");
        Add("NoneStage2boss", "대검으로 크게 내려친다","정원지");
        Add("NoneStage3boss", "기를 모아 참격을 날린다","장보");
        Add("NoneStage4boss", "기를 모아 선풍을 날린다","장량");
        Add("NoneStage5boss", "주문을 외워서 번개를 내려친다","장각");
        Add("NoneStage6boss", "지면에 검을 꽂아 폭발을 일으킨다","이각");
        Add("NoneStage7boss", "창을 빠르게 돌려 벤다","화웅");
        Add("NoneStage8boss", "활에 힘을 모아 강하게 쏜다","왕윤");
        Add("NoneStage9boss", "방천화극으로 강하게 내려친다", "여포");
        Add("NoneStage10boss", "칠성검을 뽑아 휘두른다", "동탁");
        Add("NoneStage11boss", "대검을 높이 들어 내려친다", "고순");
        Add("NoneStage12boss", "창으로 강하게 찌른다", "장료");
        Add("NoneStage13boss", "망치로 바닥을 내려친다", "진궁");
        Add("NoneStage14boss", "비파를 연주하여 공격한다", "초선");
        Add("NoneStage15boss", "방천화극으로 더욱 강하게 벤다", "여포");

        AddChangeSprite("Stage1boss");
        AddChangeSprite("Stage2boss");
        AddChangeSprite("Stage3boss");
        AddChangeSprite("Stage4boss");
        AddChangeSprite("Stage5boss");
        AddChangeSprite("Stage6boss");
        AddChangeSprite("Stage7boss");
        AddChangeSprite("Stage8boss");
        AddChangeSprite("Stage9boss");
        AddChangeSprite("Stage10boss");
        AddChangeSprite("Stage11boss");
        AddChangeSprite("Stage12boss");
        AddChangeSprite("Stage13boss");
        AddChangeSprite("Stage14boss");
        AddChangeSprite("Stage15boss");


    }

    void PrFabsproduce(int num)//
    {
        this.num = num;
        
        if (Count == 0)
        {
            rect = iconbutton[num].GetComponent<RectTransform>();
            go = Instantiate(SelectImage, rect.position, Quaternion.identity);
            go.transform.parent = iconbutton[num].transform;
            go.transform.localScale = Vector3.one;
            Bossim.gameObject.SetActive(true);
            BossName = bossinfo[num].BossName;
            NameCheck(BossName);
            UIManager.GetInstance().equipButton[num].gameObject.SetActive(true);
            prevnum = num;
            Count++;
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
            BossName = bossinfo[num].BossName;
            Bossim.gameObject.SetActive(true);
            NameCheck(BossName);
            go.transform.parent = iconbutton[num].transform;
            go.transform.position = iconbutton[num].transform.position;
            prevnum = num;
        }
    }
   
    void NameCheck(string name)
    {
        for (int i = 0; i < bossinfo.Count; i++) 
        {
            if (name == ChagneBossSprite[i].name)
            {
                Bossim.sprite = Resources.Load<Sprite>("UI/BossDictionary/Stage" + (i + 1).ToString() + "boss_im");
                BossNameTEXT.text = bossinfo[i].BossNameText;
                ContentsTEXT.text = bossinfo[i].Contents;
            }
            else if (name == bossinfo[i].BossName)
            {
                BossNameTEXT.text = "???";
                ContentsTEXT.text = "???";
                Bossim.sprite = Resources.Load<Sprite>("UI/BossDictionary/" + (i + 1));
            }
        }
            
            
        
    }
}
