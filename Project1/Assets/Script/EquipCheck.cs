using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipCheck : MonoBehaviour
{
    public bool Equip;
    public int EquipCount;
    public string Name;
    public void EquipCheck_method()
    {
        if (EquipCount == 1)
        {
            SoundManager.instance.EquipSound();
            Equip = false;
            EquipCount = 0;
            UIManager.GetInstance().equipButton[BossDictionary.GetInstance().num].GetComponent<Button>().image.sprite = Resources.Load<Sprite>("UI/BossDictionary/equip");
            FindObjectOfType<CreatureSummon>().gameObject.GetComponent<Button>().image.sprite = Resources.Load<Sprite>("UI/skill_default");
        }
        else if (EquipCount == 0)
        {
            
            Equip = true;
            if (BossDictionary.GetInstance().BossName == "")
                return;
            else if (UIManager.GetInstance().equipButton[BossDictionary.GetInstance().num].Equip == true && BossDictionary.GetInstance().bossinfo[BossDictionary.GetInstance().num].IsChange == true)
            {
                SoundManager.instance.EquipSound();
                for (int i = 0; i < UIManager.GetInstance().equipButton.Length; i++)
                {
                    if (i == BossDictionary.GetInstance().num)
                    {
                        FindObjectOfType<CreatureSummon>().gameObject.GetComponent<Button>().image.sprite = Resources.Load<Sprite>("UI/BossDictionary/Boss" + (i + 1) + "Icon");
                        UIManager.GetInstance().equipButton[i].GetComponent<Button>().image.sprite = Resources.Load<Sprite>("UI/BossDictionary/equipment");
                        EquipCount++;
                        continue;
                    }
                    UIManager.GetInstance().equipButton[i].Equip = false;
                    UIManager.GetInstance().equipButton[i].EquipCount = 0;
                    UIManager.GetInstance().equipButton[i].GetComponent<Button>().image.sprite = Resources.Load<Sprite>("UI/BossDictionary/equip");
                }

            }
            
        }
    }
}
