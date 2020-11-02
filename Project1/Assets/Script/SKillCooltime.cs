
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
    bool Attack_SpeedskillOn;
    bool SkillStart;
    
    private void Start()
    {
        DataController.GetInstance().LoadSkilltime(this);
        CrurrentTime = MaxSkillcooltime;
    }
    void Update()
    {
        AttackSpeedCooltime();
    }
    public void Attack_speedSkillOn()
    {
        skilldurationtime = 0;
        SkillStart = true;
        Attack_SpeedskillOn = true;
        Skillbutton.interactable = false;
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
}

