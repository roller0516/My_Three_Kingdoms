
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Numerics;
using Vector3 = UnityEngine.Vector3;
using System.Collections;


public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    public static UIManager GetInstance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<UIManager>();
            if (instance == null)
            {
                GameObject container = new GameObject("Canvas");
                instance = container.AddComponent<UIManager>();
            }
        }
        return instance;
    }
    public Image Backgroudimage;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI Gold;
    public TextMeshProUGUI Knowledge;
    public TextMeshProUGUI Ticket;
    public TextMeshProUGUI PaidGold;

    public float time;
    public float Starttime;
    public float Currenttime;

    public GameObject TrainingTap;
    public GameObject weaponTap;
    public GameObject TeasureTap;
    public GameObject SearchTap;
    public GameObject Timer;
    //버튼 텍스트
    
    public bool TR_Check;
    public TextMeshProUGUI[] GoldPerClickDisPlayer;
    public TextMeshProUGUI[] GoldCostClickDisPlayer;
    public TextMeshProUGUI[] Atktext;
    public TextMeshProUGUI[] WeaponCostDisPlay;
    // 버튼 갯수
    public SearchButton[] searchButtons;
    public UpgradeButton[] upgradeButton = new UpgradeButton[20]; // 훈련버튼코스트
    public Weaponcost[] weaponcost = new Weaponcost[20]; // 무기 버튼 코스트
    public TeasureCostButton[] Teasurecost_Nomal = new TeasureCostButton[6];
    public EquipCheck[] equipButton;
    public string SearchName;
    ItemList item_l;
    [HideInInspector]
    public int[] Level;
    private void Awake()
    {
        int count = 4;
        for (int i = 1; i < upgradeButton.Length + 1; i++)
        {
            upgradeButton[i - 1] = GameObject.Find("Button" + i).GetComponent<UpgradeButton>();
            upgradeButton[i - 1].UpgradeName = "Gold" + i;
            
            if (i == 1)
            {
                upgradeButton[i - 1].StartCurrentCost = "10";
            }
            else
            {
                upgradeButton[i - 1].StartCurrentCost = BigInteger.Multiply(BigInteger.Parse(upgradeButton[i - 2].StartCurrentCost.ToString()), count).ToString();
                count++;
            }
        }
    }
    private void Start()
    {
        Currenttime = Starttime;
        Level = new int[upgradeButton.Length];
        item_l = FindObjectOfType<ItemList>().GetComponent<ItemList>();
        TeasureButton();
    }
    private void Update()
    {
        PaidGold.text = DataController.GetInstance().GetPaidGold().ToString();
        for (int i=0; i < upgradeButton.Length;i++) 
        {
            Level[i] = upgradeButton[i].Level;
        }
        SearchButton();
        WeaponUpdate();
        GoldPerClickText(GoldPerClickDisPlayer);
        GoldCostClickText(GoldCostClickDisPlayer);
        WeaponCostText(WeaponCostDisPlay);
        AtkText(Atktext);
        Gold.text = GetGoldText();
        Knowledge.text = KnowledgeText();
        Ticket.text = DataController.GetInstance().GetTicket().ToString();
    }
    public void TeasureButton() 
    {
        if (TeasureTap.activeSelf == true) 
        {
            for (int i=0; i <Teasurecost_Nomal.Length;i++) 
            {
                Teasurecost_Nomal[i] = GameObject.Find("Treasure_b"+i).GetComponent<TeasureCostButton>();
            }
        }
    }
    public void WeaponUpdate() 
    {
        if (weaponTap.activeSelf == true)
        {
            for (int i = 1; i < weaponcost.Length + 1; i++)
            {
                weaponcost[i - 1] = GameObject.Find("Weapon" + i + "_b").GetComponent<Weaponcost>();
            }
            for (int i = 0; i < weaponcost.Length; i++)
            {
                if (item_l.weaponData.dataArray[i].Level < item_l.maxLevel && item_l.weaponData.dataArray[i].Level > 0) 
                {
                    item_l.bt[i].interactable = true;
                    weaponcost[i].upGradeTex.gameObject.SetActive(true);
                    weaponcost[i].im.gameObject.SetActive(true);
                    item_l.im[i].sprite = Resources.Load<Sprite>("UI/Weapon/nomalbutton");
                }
                else if (item_l.weaponData.dataArray[i].Level == 10)
                {
                    weaponcost[i].im.gameObject.SetActive(false);
                    item_l.bt[i].interactable = false;
                    item_l.im[i].sprite = Resources.Load<Sprite>("UI/Weapon/Complete");
                    weaponcost[i].upGradeTex.gameObject.SetActive(false);
                    if (i == item_l.weaponData.dataArray.Length - 1)// i가 마지막일때는 return으로 빠져나간다.
                        return;
                    else if (item_l.weaponData.dataArray[i + 1].Level == 0) 
                    {
                        weaponcost[i + 1].upGradeTex.gameObject.SetActive(true);
                        item_l.im[i + 1].sprite = Resources.Load<Sprite>("UI/Weapon/nomalbutton");
                        item_l.bt[i + 1].interactable = true;
                        weaponcost[i+1].im.gameObject.SetActive(true);
                    }
                }
            }
        }
    }

    private string GetGoldText()
    {
        int placeN = 3;
        BigInteger value = DataController.GetInstance().GetGold();
        List<int> numlist = new List<int>();
        int p = (int)Mathf.Pow(10, placeN);
        do
        {
            numlist.Add((int)(value % p));
            value /= p;
        }
        while (value>=1);
        int num = numlist.Count < 2 ? numlist[0] : numlist[numlist.Count - 1] * p + numlist[numlist.Count - 2];
        if (num < 1000)
            return num.ToString();
        float f = (num / (float)p);
        return f.ToString("N1") + GetUnitText(numlist.Count - 1);
    }
    private string KnowledgeText()
    {
        int placeN = 3;
        BigInteger value = DataController.GetInstance().GetKnowledge();
        List<int> numlist = new List<int>();
        int p = (int)Mathf.Pow(10, placeN);
        do
        {
            numlist.Add((int)(value % p));
            value /= p;
        }
        while (value >= 1);
        int num = numlist.Count < 2 ? numlist[0] : numlist[numlist.Count - 1] * p + numlist[numlist.Count - 2];
        if (num < 1000)
            return num.ToString();

        float f = (num / (float)p);
        return f.ToString("N1") + GetUnitText(numlist.Count - 1);
    }


    private string GoldPerClickText(string name)
    {
        int placeN = 3;
        BigInteger value = DataController.GetInstance().GetGoldPerClick(name);
        List<int> numlist = new List<int>();
        int p = (int)Mathf.Pow(10, placeN);
        do
        {
            numlist.Add((int)(value % p));
            value /= p;
        }
        while (value >= 1);
        int num = numlist.Count < 2 ? numlist[0] : numlist[numlist.Count - 1] * p + numlist[numlist.Count - 2];
        if (num < 1000)
            return num.ToString();

        float f = (num / (float)p);

        return f.ToString("N1") + GetUnitText(numlist.Count - 1);
    }

    private string goldCostClickText(BigInteger Cost)
    {
        int placeN = 3;
        BigInteger value = Cost;
        List<int> numlist = new List<int>();
        int p = (int)Mathf.Pow(10, placeN);
        do
        {
            numlist.Add((int)(value % p));
            value /= p;
        }
        while (value >= 1);

        int num = numlist.Count < 2 ? numlist[0] : numlist[numlist.Count - 1] * p + numlist[numlist.Count - 2];

        if (num < 1000)
            return num.ToString();

        float f = (num / (float)p);

        return f.ToString("N1") + GetUnitText(numlist.Count - 1);
    }
    private string AtkText(BigInteger Atk)
    {
        int placeN = 3;
        BigInteger value = Atk;
        List<int> numlist = new List<int>();
        int p = (int)Mathf.Pow(10, placeN);
        do
        {
            numlist.Add((int)(value % p));
            value /= p;
        }
        while (value >= 1);

        int num = numlist.Count < 2 ? numlist[0] : numlist[numlist.Count - 1] * p + numlist[numlist.Count - 2];

        if (num < 1000)
            return num.ToString();

        float f = (num / (float)p);

        return f.ToString("N1") + GetUnitText(numlist.Count - 1);
    }
    private string weaponcostText(BigInteger Cost)
    {
        int placeN = 3;
        BigInteger value = Cost;
        List<int> numlist = new List<int>();
        int p = (int)Mathf.Pow(10, placeN);
        do
        {
            numlist.Add((int)(value % p));
            value /= p;
        }
        while (value >= 1);

        int num = numlist.Count < 2 ? numlist[0] : numlist[numlist.Count - 1] * p + numlist[numlist.Count - 2];

        if (num < 1000)
            return num.ToString();

        float f = (num / (float)p);

        return f.ToString("N1") + GetUnitText(numlist.Count - 1);
    }

    private string GetUnitText(int index)
    {
        int idx = index - 1;

        if (idx < 0)
            return "";

        int repeatCount = (index / 26) + 1;

        string retstr = "";

        for (int i = 0; i < repeatCount; i++)
        {
            retstr += (char)(64 + index % 26);
        }
        return retstr;
    }

    public void GoldPerClickText(TextMeshProUGUI[] txt)
    {
        for (int i = 0; i < GoldPerClickDisPlayer.Length; i++)
        {
            txt[i].text = GoldPerClickText("GoldPerClick"+i);
        }
    }
    public void GoldCostClickText(TextMeshProUGUI[] txt)
    {
        for (int i = 0; i < GoldPerClickDisPlayer.Length; i++)
        {
            txt[i].text = goldCostClickText(upgradeButton[i].CurrentCost1);
        }
    }
    public void WeaponCostText(TextMeshProUGUI[] txt)
    {
        for (int i = 0; i < weaponcost.Length; i++)
        {
            txt[i].text = weaponcostText(weaponcost[i].CurrentCost);
        }
    }
    public void AtkText(TextMeshProUGUI[] txt)
    {
        for (int i = 0; i < item_l.weaponData.dataArray.Length; i++)
        {
            switch (item_l.weaponData.dataArray[i].Level)
            {
                case 0:
                    txt[i].text = AtkText(BigInteger.Parse(item_l.weaponData.dataArray[i].Atk));
                    break;
                case 1:
                    txt[i].text = AtkText(BigInteger.Parse(item_l.weaponData.dataArray[i].Atk_2));
                    break;
                case 2:
                    txt[i].text = AtkText(BigInteger.Parse(item_l.weaponData.dataArray[i].Atk_3));
                    break;
                case 3:
                    txt[i].text = AtkText(BigInteger.Parse(item_l.weaponData.dataArray[i].Atk_4));
                    break;
                case 4:
                    txt[i].text = AtkText(BigInteger.Parse(item_l.weaponData.dataArray[i].Atk_5));
                    break;
                case 5:
                    txt[i].text = AtkText(BigInteger.Parse(item_l.weaponData.dataArray[i].Atk_6));
                    break;
                case 6:
                    txt[i].text = AtkText(BigInteger.Parse(item_l.weaponData.dataArray[i].Atk_7));
                    break;
                case 7:
                    txt[i].text = AtkText(BigInteger.Parse(item_l.weaponData.dataArray[i].Atk_8));
                    break;
                case 8:
                    txt[i].text = AtkText(BigInteger.Parse(item_l.weaponData.dataArray[i].Atk_9));
                    break;
                case 9:
                    txt[i].text = AtkText(BigInteger.Parse(item_l.weaponData.dataArray[i].Atk_10));
                    break;
                case 10:
                    txt[i].text = AtkText(BigInteger.Parse(item_l.weaponData.dataArray[i].Atk_10));
                    break;
            }
        } 
    }
    public void SearchButton()
    {
        Enter();
        bosssummonCheck();
        if (Currenttime <= 0)
        {
            print("1");
            Timer.gameObject.SetActive(false);
            PopUpSystem.GetInstance().EnterDeongun = false;
            PopUpSystem.GetInstance().ClosePopUp();
            MonsterSpawn.GetInstance().stg.Count = 0;
            MonsterSpawn.GetInstance().stg.StageSound_p();
            Player.Instance.Monster.GetComponent<MimicEnemy>().Deth();
            time = 0;
            Currenttime = Starttime;
            Player.Instance.transform.position = new Vector3(-262.43f, Player.Instance.transform.position.y + 20f, Player.Instance.transform.position.z);
            MonsterSpawn.GetInstance().MonsterCount = 0;
            MonsterSpawn.GetInstance().transform.position = new Vector3(-254, MonsterSpawn.GetInstance().transform.position.y + 20f, MonsterSpawn.GetInstance().transform.position.z);
            MonsterSpawn.GetInstance().fade.SearchReward();
            MonsterSpawn.GetInstance().fade.Lose = true;   
        }
    }
    void bosssummonCheck() 
    {
        if (MonsterSpawn.GetInstance().BossSummonON == true)//보스 소환 
        {
            time = Time.deltaTime;
            Currenttime -= time;
            Backgroudimage.fillAmount = (Mathf.SmoothStep(0, 100, Currenttime / Starttime) / 100);
            print(Backgroudimage.fillAmount);
            timeText.text = Currenttime.ToString("0");
            if (Currenttime <= 0)
            {
                MonsterSpawn.GetInstance().BossSummonON = false;
                time = 0;
                Currenttime = Starttime;
                StartCoroutine("BossNoDeath");
                MonsterSpawn.GetInstance().stg.MonsterCount -= 4;
                if (MonsterSpawn.GetInstance().PrevMonster != null)
                    Destroy(MonsterSpawn.GetInstance().PrevMonster.gameObject, 2f);
                Timer.gameObject.SetActive(false);
            }
        }
    }
    void Enter() 
    {
        if (PopUpSystem.GetInstance().EnterDeongun == true)
        {
            MonsterSpawn.GetInstance().stg.text.text = "어느보물방...";
            time = Time.deltaTime;
            Currenttime -= time;
            Backgroudimage.fillAmount = (Mathf.SmoothStep(0, 100, Currenttime / Starttime) / 100);
            timeText.text = Currenttime.ToString("0");
        }
    }
    IEnumerator BossNoDeath()
    {
        MonsterSpawn.GetInstance().transform.position = MonsterSpawn.GetInstance().startPosition;
        MonsterSpawn.GetInstance().MonsterCount = 1;
        yield return new WaitForSeconds(1f);
        MonsterSpawn.GetInstance().fade.Fade();
        yield return new WaitForSeconds(0.5f);
        MonsterSpawn.GetInstance().MonsterCount = 0;
        Player.Instance.transform.position = Player.Instance.startPosition;
        MonsterSpawn.GetInstance().BossFail = true;
    }
}
