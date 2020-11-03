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
                BackGroud[i].GetComponent<MeshRenderer>().material = Resources.Load("Material/BackGround03", typeof(Material)) as Material;
            }
        }
        else if (curStage >= 50)
        {
            for (int i = 0; i < BackGroud.Length; i++)
            {
                BackGroud[i].GetComponent<MeshRenderer>().material = Resources.Load("Material/BackGround02", typeof(Material)) as Material;
            }
        }
        else if (curStage < 50)
        {
            for (int i = 0; i < BackGroud.Length; i++)
            {
                BackGroud[i].GetComponent<MeshRenderer>().material = Resources.Load("Material/BackGround01", typeof(Material)) as Material;
            }
        }
    }
}
