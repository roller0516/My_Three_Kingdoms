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
        Equip = true;
        if (EquipCount == 1)
        {
            Equip = false;
            EquipCount = 0;
            UIManager.GetInstance().equipButton[BossDictionary.GetInstance().num].GetComponent<Button>().image.sprite = Resources.Load<Sprite>("UI/BossDictionary/equip");
        }
        else if (EquipCount == 0)
        {
            if (UIManager.GetInstance().equipButton[BossDictionary.GetInstance().num].Equip == true)
            {
                for (int i = 0; i < UIManager.GetInstance().equipButton.Length; i++) 
                {
                    print(BossDictionary.GetInstance().num);
                    if (i == BossDictionary.GetInstance().num)
                        continue;
                    UIManager.GetInstance().equipButton[i].Equip = false;
                    UIManager.GetInstance().equipButton[i].GetComponent<Button>().image.sprite = Resources.Load<Sprite>("UI/BossDictionary/equip");
                    UIManager.GetInstance().equipButton[i].EquipCount = 0;
                }
            }
            GameObject.FindObjectOfType<CreatureSummon>().gameObject.GetComponent<Button>().image.sprite = Resources.Load<Sprite>("UI/BossDictionary/Boss" + (BossDictionary.GetInstance().num + 1) + "Icon");
            UIManager.GetInstance().equipButton[BossDictionary.GetInstance().num].GetComponent<Button>().image.sprite = Resources.Load<Sprite>("UI/BossDictionary/equipment");
            EquipCount++;
        }
    }
}
