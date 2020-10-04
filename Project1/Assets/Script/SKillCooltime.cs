
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
    bool skillOn;

    void Update()
    {
        Cooltime();
    }
    public void SkillOn()
    {
        skillOn = true;
        Skillbutton.interactable = false;
    }
    public void Cooltime()
    {
        if (skillOn == true)
        {
            skillcooltime += Time.deltaTime;
            Backgroudimage.fillAmount = 1.0f - (Mathf.SmoothStep(0, 100, skillcooltime / MaxSkillcooltime) / 100);
            cooltime.gameObject.SetActive(true);
            cooltime.text = ((int)(MaxSkillcooltime+1 - skillcooltime )).ToString();
            if (skillcooltime >= MaxSkillcooltime )
            {
                cooltime.gameObject.SetActive(false);
                skillcooltime = 0;
                skillOn = false;
            }
        }
        else if (skillOn == false)
        {
            Skillbutton.interactable = true;
        }
    }
}

