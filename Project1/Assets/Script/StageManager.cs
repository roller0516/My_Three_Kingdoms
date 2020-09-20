using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public TextMeshProUGUI[] text;
    public Slider MonsterCountSlider;
    public float MonsterCount = 1;
    public float curStage = 1;
    public float MaxStage =10;

    private void Update()
    {
        MonsterCounting();
        stageCount();
        
    }
    private void MonsterCounting()
    {
        MonsterCountSlider.value = MonsterCount / MaxStage;
    }
    private void stageCount() 
    {
        if(MonsterCount%10 >0)
            text[0].text = (MonsterCount %10).ToString() + "/" + MaxStage.ToString();
        else if (MonsterCount%MaxStage == 0)
            text[0].text = (MonsterCount/curStage).ToString() + "/" + MaxStage.ToString();
        text[1].text = curStage.ToString() + "stage";
    }
}
