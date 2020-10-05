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

    private void Update()
    {
        stageCount();
    }
 
    private void stageCount() 
    {
        //if (MonsterCount % MaxStage > 0)
        //{
        //    text[0].text = (MonsterCount % MaxStage).ToString() + "/" + MaxStage.ToString();
        //    MonsterCountSlider.value = (MonsterCount % MaxStage) / MaxStage;
        //}

        //else if (MonsterCount % MaxStage == 0) 
        //{
        //    text[0].text = (MonsterCount / curStage).ToString() + "/" + MaxStage.ToString();
        //    MonsterCountSlider.value = 1f;
        //}

        text.text = curStage.ToString() + "stage";
    }
}
