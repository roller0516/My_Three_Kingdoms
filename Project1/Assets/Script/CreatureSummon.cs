using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CreatureSummon : MonoBehaviour
{
    public TextMeshProUGUI cooltime;
    public Image Backgroudimage;
    public Button Skillbutton;
    public float skillcooltime;
    public float MaxSkillcooltime;
    public float CrurrentTime;
    public bool BossSkillOn;
    public bool BossSkillCheck;
    public GameObject[] Creatures;
    public string CreatureName;

    private void Start()
    {
        DataController.GetInstance().LoadCreatureSummon(this);
        CrurrentTime = MaxSkillcooltime;
        
    }
    public void Boss_SkillOn()
    {
        for (int i = 0; i < UIManager.GetInstance().equipButton.Length; i++)
        {
            if (UIManager.GetInstance().equipButton[i].Equip)
            {
                CreatureName = BossDictionary.GetInstance().BossName;
                if (CreatureName == Creatures[i].name)
                {
                    Instantiate(Creatures[i], new Vector3(Player.Instance.transform.position.x-1f, Player.Instance.transform.position.y, Player.Instance.transform.position.z), Quaternion.identity);
                    BossSkillOn = true;
                    Skillbutton.interactable = false;
                }
            }
        }
    }
    private void Update()
    {
        BossSummonCooltime();
    }
    public void BossSummonCooltime()
    {
        if (BossSkillOn == true)
        {
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

}
