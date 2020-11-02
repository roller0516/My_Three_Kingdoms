using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    //public Slider MonsterCountSlider;
    public float MonsterCount = 1;
    public float curStage = 1;
    public float MaxStage =10;
    public MeshRenderer[] BackGroud = new MeshRenderer[3];

    private void Update()
    {
        stageCount();
    }
 
    private void stageCount() 
    {
        text.text = curStage.ToString() + "stage";
        if (curStage >= 100)
        {
            for (int i = 0; i < BackGroud.Length; i++)
            {
                BackGroud[i].GetComponent<MeshRenderer>().materials[i] = BackGroud[i].materials[2];
            }
        }
        else if (curStage >= 50)
        {
            for (int i = 0; i < BackGroud.Length; i++)
            {
                BackGroud[i].GetComponent<MeshRenderer>().materials[i] = BackGroud[i].materials[1];
            }
        }
        else if (curStage < 50)
        {
            for (int i = 0; i < BackGroud.Length; i++)
            {
                BackGroud[i].GetComponent<MeshRenderer>().materials[i] = BackGroud[i].materials[0];
            }
        }
    }
}
