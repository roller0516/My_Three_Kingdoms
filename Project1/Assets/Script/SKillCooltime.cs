
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
    bool BossskillOn;
    bool SkillStart;
    void Update()
    {
        Cooltime();
    }
    public void SkillOn()
    {
        SkillStart = true;
        BossskillOn = true;
        Skillbutton.interactable = false;
    }
    public void Cooltime()
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
            
        if (BossskillOn == true)
        {
            skillcooltime += Time.deltaTime;
            

            Backgroudimage.fillAmount = 1.0f - (Mathf.SmoothStep(0, 100, skillcooltime / MaxSkillcooltime) / 100);
            cooltime.gameObject.SetActive(true);
            cooltime.text = ((int)(MaxSkillcooltime+1 - skillcooltime )).ToString();
           
            if (skillcooltime >= MaxSkillcooltime )
            {
                cooltime.gameObject.SetActive(false);
                skillcooltime = 0;
                BossskillOn = false;
            }
        }
        
        else if (BossskillOn == false)
        {
            Skillbutton.interactable = true;
        }
    }
}

