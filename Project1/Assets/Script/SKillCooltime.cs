
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SKillCooltime : MonoBehaviour
{
    public TextMeshProUGUI cooltime;
    public Image Backgroudimage;
    public Button Skillbutton;
    public float skillcooltime;
    public float MaxSkillcooltime;
    public float skilldurationtime;
    public float CrurrentTime;
    bool BossSkillOn;
    bool Attack_SpeedskillOn;
    bool SkillStart;
    bool BossSkillStart;
    public GameObject[] Creatures;
    public string CreatureName;
    public string PrevCreatureName;
    public bool Equip = false;
    public bool PrevEquip = false;
    
    private void Start()
    {
        CrurrentTime = MaxSkillcooltime;
    }
    void Update()
    {
        AttackSpeedCooltime();
        BossSummonCooltime();
    }
    public void Attack_speedSkillOn()
    {
        skilldurationtime = 0;
        SkillStart = true;
        Attack_SpeedskillOn = true;
        Skillbutton.interactable = false;
    }
    public void Boss_SkillOn() 
    {
        if (Equip) 
        {
            skilldurationtime = 0;
            BossSkillStart = true;
            BossSkillOn = true;
            Skillbutton.interactable = false;
        }
    }
    public void AttackSpeedCooltime()
    {
        if (SkillStart == true)
        {
            Player.Instance.Playerdata.AttackSpeed = 2.8f;
            skilldurationtime += Time.deltaTime;
            if (skilldurationtime >= 15f)
            {
                skilldurationtime = 0;
                Player.Instance.Playerdata.AttackSpeed = 1f;
                SkillStart = false;
            }
        }

        if (Attack_SpeedskillOn == true)
        {
            skillcooltime += Time.deltaTime;

            Backgroudimage.fillAmount = 1.0f - (Mathf.SmoothStep(0, 100, skillcooltime / CrurrentTime) / 100);
            cooltime.gameObject.SetActive(true);
            cooltime.text = ((int)(CrurrentTime - skillcooltime)).ToString();

            if (skillcooltime >= CrurrentTime)
            {
                cooltime.gameObject.SetActive(false);
                skillcooltime = 0;
                Attack_SpeedskillOn = false;
            }
        }
        

        else if (Attack_SpeedskillOn == false)
        {
            Skillbutton.interactable = true;
        }

       
    }
    public void BossSummonCooltime() 
    {
        if (BossSkillOn == true)
        {
            BossSkillOn = false;
            for (int i = 0; i < BossDictionary.GetInstance().bossinfo.Count;i++ ) 
            {
                if (CreatureName == Creatures[i].name)
                    Instantiate(Creatures[i],new Vector3( Player.Instance.transform.position.x - 2f, Player.Instance.transform.position.y, Player.Instance.transform.position.z), Quaternion.identity);
            }
            
            skillcooltime += Time.deltaTime;
            Backgroudimage.fillAmount = 1.0f - (Mathf.SmoothStep(0, 100, skillcooltime / CrurrentTime) / 100);
            cooltime.gameObject.SetActive(true);
            cooltime.text = ((int)(CrurrentTime - skillcooltime)).ToString();

            if (skillcooltime >= CrurrentTime)
            {
                cooltime.gameObject.SetActive(false);
                skillcooltime = 0;
                BossSkillOn = false;
            }
        }
        else if (BossSkillOn == false)
        {
            Skillbutton.interactable = true;
        }
    }
    public void EquipCheck()
    {
        Equip = true;
        if (BossDictionary.GetInstance().EquipCount == 1)
        {
            Equip = false;

            BossDictionary.GetInstance().EquipCount = 0;
            BossDictionary.GetInstance().Equipbutton[BossDictionary.GetInstance().num].image.sprite = Resources.Load<Sprite>("UI/BossDictionary/equip");
        }
        else if (Equip && BossDictionary.GetInstance().EquipCount == 0)
        {
            Equip = false;
            print(BossDictionary.GetInstance().prevnum);
            print(BossDictionary.GetInstance().num);

            if (Equip == true && BossDictionary.GetInstance().prevnum != BossDictionary.GetInstance().num)
            {
                Equip = false;
                if (Equip == true)
                {
                    BossDictionary.GetInstance().Equipbutton[BossDictionary.GetInstance().prevnum].image.sprite = Resources.Load<Sprite>("UI/BossDictionary/equip");
                    BossDictionary.GetInstance().prevnum = BossDictionary.GetInstance().num;
                }
                return;
            }
            BossDictionary.GetInstance().Equipbutton[BossDictionary.GetInstance().num].image.sprite = Resources.Load<Sprite>("UI/BossDictionary/equipment");
            BossDictionary.GetInstance().EquipCount++;
        }
        
    }
}

